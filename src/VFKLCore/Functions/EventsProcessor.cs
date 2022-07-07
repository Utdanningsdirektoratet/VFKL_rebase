using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Net.Mail;
using System.Reflection;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using System.Xml.Serialization;
using Altinn.Platform.Storage.Interface.Models;
using AltinnApplicationsOwnerSystem.Functions.VFKL.Models;
using Microsoft.Azure.Functions.Worker;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using SendGrid;
using SendGrid.Helpers.Mail;
using VFKLCore.Functions.Config;
using VFKLCore.Functions.Models;
using VFKLCore.Functions.Models.VFKL;
using VFKLCore.Functions.Models.VFKLInvitation;
using VFKLCore.Functions.Services.Interface;

namespace VFKLCore.Functions
{
    /// <summary>
    /// Azure Function responsible for downloading data for a given instance.
    /// Triggered by CloudEvent on Azure Queue
    /// When finished it forward CloudEvent to confirmation queue
    /// </summary>
    public class EventsProcessor
    {
        private readonly IAltinnApp _altinnApp;

        private readonly IPlatform _platform;

        private readonly IStorage _storage;

        private readonly IQueueService _queueService;

        private static ILogger _logger;

        private readonly VFKLCoreSettings _settings;

        /// <summary>
        /// Initializes a new instance of the <see cref="EventsProcessor"/> class.
        /// </summary>
        public EventsProcessor(
            IAltinnApp altinnApp,
            IPlatform platform,
            IStorage storage,
            IQueueService queueService,
            ILogger<EventsProcessor> logger,
            IOptions<VFKLCoreSettings> altinnIntegratorSettings)
        {
            _altinnApp = altinnApp;
            _platform = platform;
            _storage = storage;
            _queueService = queueService;
            _logger = logger;
            _settings = altinnIntegratorSettings.Value;
        }

        /// <summary>
        /// Reads cloud event from events-inbound queue and download instance and data for that given event and store it to configured azure storage
        /// </summary>
        [Function(nameof(EventsProcessor))]
        public async Task Run([Microsoft.Azure.Functions.Worker.QueueTrigger("events-inbound", Connection = "QueueStorageSettings:ConnectionString")] string item, FunctionContext executionContext)
        {
            CloudEvent cloudEvent = JsonSerializer.Deserialize<CloudEvent>(item);

            if (!ShouldProcessEvent(cloudEvent))
            {
                return;
            }

            (string appId, string instanceId) appInfo = GetInstanceInfoFromSource(cloudEvent.Source);
            Instance instance = await _altinnApp.GetInstance(appInfo.appId, appInfo.instanceId);

            string instanceGuid = instance.Id.Split("/")[1];
            string instanceFolder = instance.AppId + "/" + instanceGuid + "/";
            string instancePath = instanceFolder + instanceGuid;
            
            await _storage.SaveBlob(instancePath, JsonSerializer.Serialize(instance));

            _logger.LogInformation("Event type: " + cloudEvent.Type);
            _logger.LogInformation("Event Id: " + cloudEvent.Id);

            if (instance.AppId.Contains("invitasjon"))
            {
                await ProcessVFKLInvitasjon(instance);
            }
            else
            {
                await ProcessVFKL(instance, instanceGuid);
            }

            // Push to confirmation queue 
            await _queueService.PushToConfirmationQueue(JsonSerializer.Serialize(cloudEvent));
        }

        private async Task ProcessVFKL(Instance instance, string instanceGuid)
        {
            Vurdering answers = null;
            foreach (DataElement data in instance.Data)
            {
                ResourceLinks links = data.SelfLinks;
                using (Stream stream = await _platform.GetBinaryData(links.Platform))
                {
                    await _storage.UploadFromStreamAsync(data.BlobStoragePath, stream);
                }

                if (data.ContentType == "application/xml")
                {
                    XmlSerializer reader = new XmlSerializer(typeof(Vurdering));
                    answers = (Vurdering)reader.Deserialize(await _platform.GetBinaryData(links.Platform));
                    _logger.LogInformation("ProcessVFKL - Læremiddel: " + answers.Læremiddel);
                    _logger.LogInformation("ProcessVFKL - Instanceid: " + data.InstanceGuid);
                    answers.VurderingsID = data.InstanceGuid;
                }
            }

            if (answers != null)
            {
                await SaveAssessment(answers);
                int assessmentId = await GetAssessmentId(instanceGuid);

                switch (answers.VurderingsType)
                {
                    case "AlleFag":
                        if (answers.AlleFag.Del1.OnskerIkkeSvare == "true")
                        {
                            await SaveAnswer(new Paastand { Svar = AnswerType.NotApplicable.ToString(), Kommentar = string.Empty }, assessmentId, 1);
                        }
                        else
                        {
                            await SaveAnswer(answers.AlleFag.Del1.Paastand1, assessmentId, 4);
                            await SaveAnswer(answers.AlleFag.Del1.Paastand2, assessmentId, 5);
                            await SaveAnswer(answers.AlleFag.Del1.Paastand3, assessmentId, 6);
                            await SaveAnswer(answers.AlleFag.Del1.Paastand4, assessmentId, 7);
                        }

                        if (answers.AlleFag.Del2.OnskerIkkeSvare == "true")
                        {
                            await SaveAnswer(new Paastand { Svar = AnswerType.NotApplicable.ToString(), Kommentar = string.Empty }, assessmentId, 2);
                        }
                        else
                        {
                            await SaveAnswer(answers.AlleFag.Del2.Paastand1, assessmentId, 8);
                            await SaveAnswer(answers.AlleFag.Del2.Paastand2, assessmentId, 9);
                            await SaveAnswer(answers.AlleFag.Del2.Paastand3, assessmentId, 10);
                            await SaveAnswer(answers.AlleFag.Del2.Paastand4, assessmentId, 11);
                            await SaveAnswer(answers.AlleFag.Del2.Paastand5, assessmentId, 12);
                            await SaveAnswer(answers.AlleFag.Del2.Paastand6, assessmentId, 13);
                        }

                        if (answers.AlleFag.Del3.OnskerIkkeSvare == "true")
                        {
                            await SaveAnswer(new Paastand { Svar = AnswerType.NotApplicable.ToString(), Kommentar = string.Empty }, assessmentId, 3);
                        }
                        else
                        {
                            await SaveAnswer(answers.AlleFag.Del3.Paastand1, assessmentId, 14);
                            await SaveAnswer(answers.AlleFag.Del3.Paastand2, assessmentId, 15);
                            await SaveAnswer(answers.AlleFag.Del3.Paastand3, assessmentId, 16);
                            await SaveAnswer(answers.AlleFag.Del3.Paastand4, assessmentId, 17);
                            await SaveAnswer(answers.AlleFag.Del3.Paastand5, assessmentId, 18);
                            await SaveAnswer(answers.AlleFag.Del3.Paastand6, assessmentId, 19);
                            await SaveAnswer(answers.AlleFag.Del3.Paastand7, assessmentId, 20);
                            await SaveAnswer(answers.AlleFag.Del3.Paastand8, assessmentId, 21);
                        }

                        break;
                    case "Engelsk":
                        await SaveAnswer(answers.Engelsk.Del1.Paastand1, assessmentId, 22);
                        await SaveAnswer(answers.Engelsk.Del1.Paastand2, assessmentId, 23);
                        await SaveAnswer(answers.Engelsk.Del1.Paastand3, assessmentId, 24);
                        await SaveAnswer(answers.Engelsk.Del1.Paastand4, assessmentId, 25);
                        await SaveAnswer(answers.Engelsk.Del1.Paastand5, assessmentId, 26);
                        await SaveAnswer(answers.Engelsk.Del1.Paastand6, assessmentId, 27);
                        await SaveAnswer(answers.Engelsk.Del1.Paastand7, assessmentId, 28);
                        await SaveAnswer(answers.Engelsk.Del1.Paastand8, assessmentId, 29);
                        await SaveAnswer(answers.Engelsk.Del1.Paastand9, assessmentId, 30);
                        await SaveAnswer(answers.Engelsk.Del1.Paastand10, assessmentId, 31);
                        await SaveAnswer(answers.Engelsk.Del1.Paastand11, assessmentId, 32);
                        await SaveAnswer(answers.Engelsk.Del1.Paastand12, assessmentId, 33);
                        await SaveAnswer(answers.Engelsk.Del2.Paastand1, assessmentId, 34);
                        await SaveAnswer(answers.Engelsk.Del2.Paastand2, assessmentId, 35);
                        await SaveAnswer(answers.Engelsk.Del2.Paastand3, assessmentId, 36);
                        await SaveAnswer(answers.Engelsk.Del2.Paastand4, assessmentId, 37);
                        await SaveAnswer(answers.Engelsk.Del2.Paastand5, assessmentId, 38);
                        await SaveAnswer(answers.Engelsk.Del2.Paastand6, assessmentId, 39);
                        await SaveAnswer(answers.Engelsk.Del2.Paastand7, assessmentId, 40);
                        await SaveAnswer(answers.Engelsk.Del2.Paastand8, assessmentId, 41);
                        await SaveAnswer(answers.Engelsk.Del2.Paastand9, assessmentId, 42);
                        await SaveAnswer(answers.Engelsk.Del2.Paastand10, assessmentId, 43);
                        await SaveAnswer(answers.Engelsk.Del2.Paastand11, assessmentId, 44);
                        await SaveAnswer(answers.Engelsk.Del2.Paastand12, assessmentId, 45);
                        await SaveAnswer(answers.Engelsk.Del2.Paastand13, assessmentId, 46);
                        await SaveAnswer(answers.Engelsk.Del2.Paastand14, assessmentId, 47);
                        await SaveAnswer(answers.Engelsk.Del3.Paastand1, assessmentId, 48);
                        await SaveAnswer(answers.Engelsk.Del3.Paastand2, assessmentId, 49);
                        await SaveAnswer(answers.Engelsk.Del3.Paastand3, assessmentId, 50);
                        await SaveAnswer(answers.Engelsk.Del3.Paastand4, assessmentId, 51);
                        break;

                    case "Matte":
                        await SaveAnswer(answers.Matte.Del1.Paastand1, assessmentId, 52);
                        await SaveAnswer(answers.Matte.Del1.Paastand2, assessmentId, 53);
                        await SaveAnswer(answers.Matte.Del1.Paastand3, assessmentId, 54);
                        await SaveAnswer(answers.Matte.Del1.Paastand4, assessmentId, 55);
                        await SaveAnswer(answers.Matte.Del1.Paastand5, assessmentId, 56);
                        await SaveAnswer(answers.Matte.Del1.Paastand6, assessmentId, 57);
                        await SaveAnswer(answers.Matte.Del1.Paastand7, assessmentId, 58);
                        await SaveAnswer(answers.Matte.Del2.Paastand1, assessmentId, 59);
                        await SaveAnswer(answers.Matte.Del2.Paastand2, assessmentId, 60);
                        await SaveAnswer(answers.Matte.Del2.Paastand3, assessmentId, 61);
                        await SaveAnswer(answers.Matte.Del2.Paastand4, assessmentId, 62);
                        await SaveAnswer(answers.Matte.Del2.Paastand5, assessmentId, 63);
                        await SaveAnswer(answers.Matte.Del2.Paastand6, assessmentId, 64);
                        await SaveAnswer(answers.Matte.Del3.Paastand1, assessmentId, 65);
                        await SaveAnswer(answers.Matte.Del3.Paastand2, assessmentId, 66);
                        await SaveAnswer(answers.Matte.Del3.Paastand3, assessmentId, 67);
                        await SaveAnswer(answers.Matte.Del3.Paastand4, assessmentId, 68);
                        await SaveAnswer(answers.Matte.Del3.Paastand5, assessmentId, 69);
                        await SaveAnswer(answers.Matte.Del3.Paastand6, assessmentId, 70);
                        await SaveAnswer(answers.Matte.Del3.Paastand7, assessmentId, 71);
                        await SaveAnswer(answers.Matte.Del3.Paastand8, assessmentId, 72);
                        await SaveAnswer(answers.Matte.Del3.Paastand9, assessmentId, 73);
                        await SaveAnswer(answers.Matte.Del3.Paastand10, assessmentId, 74);
                        await SaveAnswer(answers.Matte.Del3.Paastand11, assessmentId, 75);
                        break;

                    case "Norsk":
                        await SaveAnswer(answers.Norsk.Del1.Paastand1, assessmentId, 76);
                        await SaveAnswer(answers.Norsk.Del1.Paastand2, assessmentId, 77);
                        await SaveAnswer(answers.Norsk.Del1.Paastand3, assessmentId, 78);
                        await SaveAnswer(answers.Norsk.Del1.Paastand4, assessmentId, 79);
                        await SaveAnswer(answers.Norsk.Del1.Paastand5, assessmentId, 80);
                        await SaveAnswer(answers.Norsk.Del1.Paastand6, assessmentId, 81);
                        await SaveAnswer(answers.Norsk.Del1.Paastand7, assessmentId, 82);
                        await SaveAnswer(answers.Norsk.Del1.Paastand8, assessmentId, 83);
                        await SaveAnswer(answers.Norsk.Del1.Paastand9, assessmentId, 84);
                        await SaveAnswer(answers.Norsk.Del1.Paastand10, assessmentId, 85);
                        await SaveAnswer(answers.Norsk.Del1.Paastand11, assessmentId, 86);
                        await SaveAnswer(answers.Norsk.Del1.Paastand12, assessmentId, 87);
                        await SaveAnswer(answers.Norsk.Del1.Paastand13, assessmentId, 88);
                        await SaveAnswer(answers.Norsk.Del1.Paastand14, assessmentId, 89);
                        await SaveAnswer(answers.Norsk.Del2.Paastand1, assessmentId, 90);
                        await SaveAnswer(answers.Norsk.Del2.Paastand2, assessmentId, 91);
                        await SaveAnswer(answers.Norsk.Del2.Paastand3, assessmentId, 92);
                        await SaveAnswer(answers.Norsk.Del2.Paastand4, assessmentId, 93);
                        await SaveAnswer(answers.Norsk.Del3.Paastand1, assessmentId, 94);
                        await SaveAnswer(answers.Norsk.Del3.Paastand2, assessmentId, 95);
                        await SaveAnswer(answers.Norsk.Del3.Paastand3, assessmentId, 96);
                        await SaveAnswer(answers.Norsk.Del3.Paastand4, assessmentId, 97);
                        await SaveAnswer(answers.Norsk.Del3.Paastand5, assessmentId, 98);
                        break;

                    default:
                        break;
                }
            }
        }

        private async Task ProcessVFKLInvitasjon(Instance instance)
        {
            GruppeInvitasjon invitation = null;
            foreach (DataElement data in instance.Data)
            {
                ResourceLinks links = data.SelfLinks;

                using (Stream stream = await _platform.GetBinaryData(links.Platform))
                {
                    await _storage.UploadFromStreamAsync(data.BlobStoragePath, stream);
                }

                if (data.ContentType == "application/xml")
                {
                    XmlSerializer reader = new XmlSerializer(typeof(GruppeInvitasjon));
                    invitation = (GruppeInvitasjon)reader.Deserialize(await _platform.GetBinaryData(links.Platform));

                    if (invitation != null)
                    {
                        string assessmenttypeString = invitation.VurderingsType.ToLower(); // For use in email text
                        if (assessmenttypeString == "allefag") { assessmenttypeString = "alle fag"; }

                        await SaveGroupInvitation(invitation);

                        if (!string.IsNullOrWhiteSpace(invitation.MottakerEposter))
                        {
                            string[] eposter = invitation.MottakerEposter.Split(';');

                            EmailAddress from = new EmailAddress(_settings.EmailAccount, _settings.EmailAccountName);

                            List<EmailAddress> recipients = new List<EmailAddress>();
                            foreach (string epost in eposter)
                            {
                                recipients.Add(new EmailAddress(epost));
                            }

                            StringBuilder body = new StringBuilder();

                            body.Append("Hei<br><br>");
                            body.Append($"{invitation.Navn} har invitert deg til å delta i en vurdering av et læremiddel.<br><br>");
                            body.Append($"Læremiddelet skal vurderes for bruk iht. følgende læreplan: {invitation.Læreplan} ({invitation.LæreplanKode})<br>");
                            body.Append($"Læremiddelet som skal vurderes er: {invitation.Læremiddel}<br><br>");
                            body.Append($"For å vurdere læremiddelet brukes tjenesten «Veileder for vurdering av læremidler i {assessmenttypeString}» fra Utdanningsdirektoratet. Når du følger lenken for å starte din vurdering blir du bedt om å logge inn på din Feide konto for å bruke tjenesten.<br><br>");

                            DateTime frist;
                            if (DateTime.TryParse(invitation.VurderingsFrist, out frist))
                            {
                                body.Append($"Frist for vurderingen er: {frist.ToString("D", new CultureInfo("no"))}<br><br>");
                            }
                            else
                            {
                                body.Append($"Frist for vurderingen er: {invitation.VurderingsFrist} <br><br>");
                            }

                            body.Append($"<a href=\"{_settings.EmailUrl}{invitation.GruppeVurderingsID}\">Klikk her for å starte din vurdering</a><br><br>");
                            body.Append($"Du kan ikke svare på denne eposten. Dersom du har spørsmål, ta kontakt med {invitation.Navn}. Dersom du opplever tekniske utfordringer, kontakt vfklsupport@udir.no");

                            SendGridClient client = new SendGridClient(_settings.SendGridApiKey);

                            try
                            {
                                SendGridMessage msg = MailHelper.CreateSingleEmailToMultipleRecipients(from, recipients, $"Vurdering av læremiddelet {invitation.Læremiddel} (no-reply)", null, body.ToString());
                                Response response = await client.SendEmailAsync(msg).ConfigureAwait(false);
                                if (response != null && !response.IsSuccessStatusCode)
                                {
                                    _logger.LogError(response.StatusCode.ToString());
                                }
                            }
                            catch (Exception ex)
                            {
                                _logger.LogError("Error sending email: " + ex.Message);
                            }
                        }
                    }
                }
            }
        }

        /// <summary>
        /// Save invitation to database
        /// </summary>
        private async Task SaveGroupInvitation(GruppeInvitasjon invitation)
        {
            UserProfile user = new UserProfile();
            user.FeideId = invitation.BrukerID;
            user.Name = invitation.Navn;
            _logger.LogInformation($"name: {user.Name}");
            await _storage.SaveUser(user);
            user = await _storage.GetUser(invitation.BrukerID);
            invitation.BrukerID = user.UserID.ToString();
            AssessmentType assessmentType = (AssessmentType)Enum.Parse(typeof(AssessmentType), invitation.VurderingsType);
            invitation.VurderingsType = ((int)assessmentType).ToString();
            string[] laereplan = string.IsNullOrEmpty(invitation.Læreplan) ? null : invitation.Læreplan.Split(';');
            invitation.Læreplan = string.IsNullOrEmpty(laereplan[0]) ? null : laereplan[0].Trim();
            invitation.LæreplanKode = string.IsNullOrEmpty(laereplan[1]) ? null : laereplan[1].Trim();
            await _storage.SaveGroupInvitation(invitation);
        }

        /// <summary>
        /// Save answer to database
        /// </summary>
        private async Task SaveAssessment(Vurdering vurdering)
        {
            await SaveUser(vurdering.BrukerID, vurdering.Navn);
            await _storage.SaveAssessment(vurdering);
        }

        /// <summary>
        /// Save answer to database
        /// </summary>
        private async Task SaveAnswer(Paastand paastand, int assessmentId, int questionId)
        {
            if (!paastand.ValgtBortForGruppe)
            {
                await _storage.SaveAnswer(paastand, assessmentId, questionId);
                _logger.LogInformation($"saved answer for assessment {assessmentId} and question : {questionId}");
            }
        }

        /// <summary>
        /// Save answer to database
        /// </summary>
        private async Task<int> GetAssessmentId(string instanceId)
        {
            int assessmentId = await _storage.GetAssessmentId(instanceId);
            _logger.LogInformation("AssessmentId:" + assessmentId);
            return assessmentId;
        }

        /// <summary>
        /// Get user
        /// </summary>
        private async Task<UserProfile> GetUser(string feideId)
        {
            UserProfile user = await _storage.GetUser(feideId);
            _logger.LogInformation("feideID:" + feideId);
            return user;
        }

        /// <summary>
        /// Save user to database
        /// </summary>
        private async Task SaveUser(string brukerId, string name)
        {
            UserProfile user = new UserProfile();
            user.FeideId = brukerId;
            user.Name = name;
            await _storage.SaveUser(user);
            _logger.LogInformation($"saved user");
        }

        /// <summary>
        /// Creates an instance for a given event
        /// </summary>
        private (string, string) GetInstanceInfoFromSource(Uri eventUri)
        {
            string[] parts = eventUri.Segments;
            (string appId, string instanceId) appInfo = ($"{parts[1]}{parts[2]}", $"{parts[4]}{parts[5]}");
            return appInfo;
        }

        /// <summary>
        ///  Will based on configuration decide if the event need to be processed. Todo add logic
        /// </summary>
        private bool ShouldProcessEvent(CloudEvent cloudEvent)
        {
            if (cloudEvent.Type == "platform.events.validatesubscription")
            {
                return false;
            }

            return true;
        }
    }
}
