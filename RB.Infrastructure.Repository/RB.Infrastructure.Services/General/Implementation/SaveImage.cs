using Microsoft.AspNetCore.Http;
using RB.Infrastructure.RB.Infrastructure.Services.General.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RB.Core.Application.DTOModel;
using Microsoft.AspNetCore.Hosting;


namespace RB.Infrastructure.RB.Infrastructure.Services.General.Implementation
{
    public class SaveImage : ISaveImage
    {
        private readonly IWebHostEnvironment _hostEnvironment;
        public SaveImage(IWebHostEnvironment hostEnvironment)
        {
            _hostEnvironment = hostEnvironment;
        }

  

        public string UploadImage(IFormFile logo , string name)
        {
            if(logo != null)
            {
                string imagePath = new String(Path.GetFileNameWithoutExtension(logo.FileName)).Replace(' ', '-');
                imagePath = name + "-" + imagePath + DateTime.Now.ToString("yyyyMMddhhmmfff") + Path.GetExtension(logo.FileName);
                var path = Path.Combine(_hostEnvironment.ContentRootPath, "Images", imagePath);

                using (var filestream = new FileStream(path, FileMode.Create))
                {
                    logo.CopyToAsync(filestream);
                }
                return imagePath;
            }
            else
            {
                return "";
            }
            
            
        }

       
    }
}
