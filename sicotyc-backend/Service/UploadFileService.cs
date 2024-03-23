using AutoMapper;
using Contracts;
using Entities.DataTransferObjects;
using Entities.Models;
using Microsoft.AspNetCore.Identity;
using Service.Contracts;
using System.Globalization;

namespace Service
{
    public class UploadFileService : IUploadFileService
    {        
        private readonly ILoggerManager _logger;
        private readonly IMapper _mapper;
        private readonly UserManager<User> _userManager;
        private readonly IAuthenticationManager _authManager;
        private readonly IRepositoryManager _respository;

        public UploadFileService(ILoggerManager logger, IMapper mapper,
            UserManager<User> userManager, IAuthenticationManager authManager, IRepositoryManager repository)
        {
            _logger = logger;
            _mapper = mapper;
            _userManager = userManager;
            _authManager = authManager;
            _respository = repository;
        }        

        public async Task<bool> UpdateImageAsync(string type, Guid id, string rootPath, string fileName)
        {
            switch (type.ToUpper())
            {
                case "USERS":
                    var userDB = await _userManager.FindByIdAsync(id.ToString());
                    if (userDB == null)
                    {
                        _logger.LogError($"Usuario con id {id} no existe en la BD");
                        return false;
                    }                    

                    string oldPath = Path.Combine(rootPath, "Uploads", type!, userDB.Img == null ? string.Empty : userDB.Img);

                    this.DeleteImage(oldPath);                  
                    
                    userDB.Img = fileName;
                    await _userManager.UpdateAsync(userDB);
                    return true;

                    //break;
                case "TRANSPORTS":
                    return true;
                    //break;

                default: 
                    return false;
            }
        }

        public async Task<bool> DeleteImageAsync(string type, string rootPath, string fileName)
        {
            string oldPath = Path.Combine(rootPath, "Uploads", type!, fileName);
            this.DeleteImage(oldPath);
            await Task.Delay(100);
            return true;
        }

        private void DeleteImage(string oldPath) {            
            if (File.Exists(oldPath))
            {
                // Borrar la imagen anterior
                File.Delete(oldPath);
            }
        }
    }
}
