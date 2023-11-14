using Amazon.Runtime;
using Amazon.S3;
using Amazon.S3.Transfer;
using Microsoft.AspNetCore.Identity;
using SampleLMS.Controllers;
using SampleLMS.Models.S3;

namespace SampleLMS.Services
{
    public class StorageService : IStorageServiceInterface
    {
        private readonly UserManager<IdentityUser> userManager;
        private readonly ILogger<FileUploadsController> logger;
        private readonly IConfiguration configuration;

        public StorageService(
                ILogger<FileUploadsController> logger,
                IConfiguration configuration,
                UserManager<IdentityUser> userManager
            )
        {
            this.logger = logger;
            this.configuration = configuration;
            this.userManager = userManager;
        }

        public async Task<S3ResponseDto> UploadFileAsync(List<IFormFile> files)
        {
            
            // specify the region
            var config = new AmazonS3Config()
            {
                RegionEndpoint = Amazon.RegionEndpoint.USEast1
            };

            var response = new S3ResponseDto();
            foreach ( var file in files )
            {
                // process the file
                await using var memoryStream = new MemoryStream();
                await file.CopyToAsync(memoryStream);

                var fileExt = Path.GetExtension(file.FileName);
                var objName = $"{Guid.NewGuid()}.{fileExt}";

                var s3Obj = new S3Object()
                {
                    BucketName = "dotnet-sample-lms-demo",
                    InputStream = memoryStream,
                    Name = objName
                };


                var creds = new AwsCredentials()
                {
                    AwsKey = configuration["AwsConfiguration:AWSAccessKey"],
                    AwsSecretKey = configuration["AwsConfiguration:AWSSecretKey"]
                };

                // Adding AWS credentials
                var credentials = new BasicAWSCredentials(creds.AwsKey,
                                                          creds.AwsSecretKey);
                try
                {
                    var uploadRequest = new TransferUtilityUploadRequest()
                    {
                        InputStream = s3Obj.InputStream,
                        Key = s3Obj.Name,
                        BucketName = s3Obj.BucketName,
                        CannedACL = S3CannedACL.NoACL
                    };
                    var s3URL = $"https://{s3Obj.BucketName}.s3.amazonaws.com/{uploadRequest.Key}";

                    // created an s3 client
                    using var client = new AmazonS3Client(credentials, config);

                    // upload utility to s3
                    var transferUtility = new TransferUtility(client);

                    // actually uploading the file to S3
                    await transferUtility.UploadAsync(uploadRequest);

                    response.StatusCode = 200;
                    response.Message = $"{s3Obj.Name} has been uploaded successfully";
                    response.FileURLs.Add(s3URL);
                }
                catch (AmazonS3Exception ex)
                {
                    response.StatusCode = (int)ex.StatusCode;
                    response.Message = $"Amazon S3 Exception: {ex.Message}";
                }
                catch (Exception ex)
                {
                    response.StatusCode = 500;
                    response.Message = ex.Message;
                }
            }
            return response;
        }
    }
}
