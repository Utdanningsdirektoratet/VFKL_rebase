using System;
using System.IO;
using System.Threading.Tasks;
using AltinnApplicationsOwnerSystem.Functions.VFKL.Models;
using Azure.Storage;
using Azure.Storage.Blobs;
using Azure.Storage.Blobs.Models;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Npgsql;
using NpgsqlTypes;
using VFKLCore.Functions.Config;
using VFKLCore.Functions.Models.VFKL;
using VFKLCore.Functions.Models.VFKLInvitation;
using VFKLCore.Functions.Repository;
using VFKLCore.Functions.Services.Interface;
using VFKLCore.Models.VFKL;

namespace VFKLCore.Functions.Services.Implementation
{
    /// <summary>
    /// Class that handles integration with Azure Blob Storage.
    /// </summary>
    public class StorageService : IStorage
    {
        private readonly VFKLCoreSettings _settings;
        private readonly KeyVaultSettings _vaultSettings;
        private readonly ILogger _logger;
        private readonly IKeyVaultService _keyVaultService;
        private readonly IAssessmentRepository _assessmentRepository;
        private readonly IAnswerRepository _answerRepository;
        private readonly IGroupInvitationRepository _groupInvitationRepository;

        private readonly IUserRepository _userRepository;

        /// <summary>
        /// Initializes a new instance of the <see cref="StorageService"/> class.
        /// </summary>
        public StorageService(
            IOptions<VFKLCoreSettings> altinnIntegratorSettings, 
            IOptions<KeyVaultSettings> vaultSettings, 
            ILogger<StorageService> logger, 
            IKeyVaultService keyVaultService, 
            IAssessmentRepository assessmentRepository,
            IAnswerRepository answerRepository,
            IGroupInvitationRepository groupInvitationRepository,
            IUserRepository userRepository)
        {
            _settings = altinnIntegratorSettings.Value;
            _vaultSettings = vaultSettings.Value;
            _logger = logger;
            _keyVaultService = keyVaultService;
            _assessmentRepository = assessmentRepository;
            _answerRepository = answerRepository;
            _groupInvitationRepository = groupInvitationRepository;
            _userRepository = userRepository;
        }

        /// <inheritdoc />
        public async Task DeleteBlobFromContainer(string name, string container)
        {
            BlobClient client;

            StorageSharedKeyCredential storageCredentials = new StorageSharedKeyCredential(_settings.AccountName, _settings.AccountKey);
            BlobServiceClient serviceClient = new BlobServiceClient(new Uri(_settings.BlobEndpoint), storageCredentials);
            BlobContainerClient blobContainerClient = serviceClient.GetBlobContainerClient(container);
            client = blobContainerClient.GetBlobClient(name);

            await client.DeleteIfExistsAsync();
        }

        /// <summary>
        /// Saves data in blob storage defined in configuration.
        /// </summary>
        public async Task SaveBlob(string name, string data)
        {
            try
            {
                BlobClient client;

                StorageSharedKeyCredential storageCredentials = new StorageSharedKeyCredential(_settings.AccountName, _settings.AccountKey);
                BlobServiceClient serviceClient = new BlobServiceClient(new Uri(_settings.BlobEndpoint), storageCredentials);
                BlobContainerClient blobContainerClient = serviceClient.GetBlobContainerClient(_settings.StorageContainer);

                client = blobContainerClient.GetBlobClient(name);

                Stream stream = new MemoryStream();
                StreamWriter writer = new StreamWriter(stream);
                writer.Write(data);
                writer.Flush();
                stream.Position = 0;
                await client.UploadAsync(stream, true);
                stream.Dispose();
            }
            catch (Exception e)
            {
                _logger.LogError(e, "Storageservice exception: SaveBlob");
                throw;
            }
        }

        /// <inheritdoc />
        public async Task SaveRegisteredSubscription(string name, Subscription subscription)
        {
            BlobClient client;

            StorageSharedKeyCredential storageCredentials = new StorageSharedKeyCredential(_settings.AccountName, _settings.AccountKey);
            BlobServiceClient serviceClient = new BlobServiceClient(new Uri(_settings.BlobEndpoint), storageCredentials);
            BlobContainerClient blobContainerClient = serviceClient.GetBlobContainerClient(_settings.RegisteredSubStorageContainer);

            client = blobContainerClient.GetBlobClient(name);

            Stream stream = new MemoryStream();
            StreamWriter writer = new StreamWriter(stream);
            writer.Write(subscription.ToJson());
            writer.Flush();
            stream.Position = 0;
            await client.UploadAsync(stream, true);
            stream.Dispose();
        }

        /// <inheritdoc/>
        public async Task<long> UploadFromStreamAsync(string name, Stream stream)
        {
            try
            {
                BlobClient blockBlob = CreateBlobClient(name);

                await blockBlob.UploadAsync(stream, true);
                BlobProperties properties = await blockBlob.GetPropertiesAsync();

                return properties.ContentLength;
            }
            catch (Exception e)
            {
                _logger.LogError(e, "Storageservice exception: UploadFromStreamAsync");
                throw;
            }
        }

        /// <inheritdoc/>
        public async Task SaveGroupInvitation(GruppeInvitasjon invitation)
        {
            try
            {
                await _groupInvitationRepository.Create(invitation);
            }
            catch (Exception e)
            {
                _logger.LogError(e, "Storageservice exception: save invitation");
                throw;
            }
        }

        /// <inheritdoc/>
        public async Task SaveAssessment(Vurdering vurdering)
        {
            try
            {
                UserProfile user = await GetUser(vurdering.BrukerID);
                int userId = user != null ? user.UserID : 0;
                Assessment assessment = new Assessment();
                assessment.TeachingAid = vurdering.Læremiddel;
                assessment.InstanceId = Guid.Parse(vurdering.VurderingsID);
                assessment.UserId = userId;
                assessment.GroupAssesmentId = vurdering.GruppeVurderingsID;
                assessment.UserComments = vurdering.OppsummeringsKommentar;
                assessment.TeachingAidSupplier = vurdering.LæremiddelLeverandør;
                await _assessmentRepository.Create(assessment);
            }
            catch (Exception e)
            {
                _logger.LogError(e, "Storageservice exception : Save assessment");
                throw;
            }
        }

        /// <inheritdoc/>
        public async Task SaveAnswer(Paastand paastand, int assesmentId, int questionId)
        {
            try
            {
                Answers answer = new Answers();
                answer.AssessmentId = assesmentId;
                answer.QuestionId = questionId;
                answer.AnswerTypeId = (AnswerType)Enum.Parse(typeof(AnswerType), paastand.Svar);
                answer.Reason = paastand.Kommentar;
                await _answerRepository.Create(answer);
            }
            catch (Exception e)
            {
                _logger.LogError(e, "Storageservice exception");
                throw;
            }
        }

        /// <inheritdoc/>
        public async Task<int> GetAssessmentId(string instanceId)
        {
            try
            {
                int assessmentId = await _assessmentRepository.GetAssessmentId(instanceId);
                return assessmentId;
            }
            catch (Exception e)
            {
                _logger.LogError(e, "Storageservice exception : Get Assessment");
                throw;
            }
        }

        /// <inheritdoc/>
        public async Task SaveUser(UserProfile user)
        {
            try
            {
                await _userRepository.Create(user);
            }
            catch (Exception e)
            {
                _logger.LogError(e, "Storageservice exception : Save user");
                throw;
            }
        }

        /// <inheritdoc/>
        public async Task<UserProfile> GetUser(string feideId)
        {
            try
            {
                UserProfile user = await _userRepository.Get(feideId);
                return user;
            }
            catch (Exception e)
            {
                _logger.LogError(e, "Storageservice exception - Get user");
                throw;
            }
        }

        /// <inheritdoc/>
        public async Task<GruppeInvitasjon> GetInvitation(string groupAssessmentId)
        {
            try
            {
                GruppeInvitasjon invitation = await _groupInvitationRepository.GetInvitation(groupAssessmentId);
                return invitation;
            }
            catch (Exception e)
            {
                _logger.LogError(e, "Storageservice exception - Get Invitation");
                throw;
            }
        }

        private BlobClient CreateBlobClient(string blobName)
        {
            BlobClient client;

            StorageSharedKeyCredential storageCredentials = new StorageSharedKeyCredential(_settings.AccountName, _settings.AccountKey);
            BlobServiceClient serviceClient = new BlobServiceClient(new Uri(_settings.BlobEndpoint), storageCredentials);
            BlobContainerClient blobContainerClient = serviceClient.GetBlobContainerClient(_settings.StorageContainer);

            client = blobContainerClient.GetBlobClient(blobName);

            return client;
        }
    }
}
