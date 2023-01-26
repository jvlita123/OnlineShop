using Sklep_MVC_Projekt.Models;
using Sklep_MVC_Projekt.Repositories;

namespace Sklep_MVC_Projekt.Services
{
    public class PhotoService
    {
        private readonly PhotoRepository _photoRepository;

        public PhotoService(PhotoRepository photoRepository)
        {
            _photoRepository = photoRepository;
        }

        public List<Photo> GetAll()
        {
            List<Photo> photos = _photoRepository.GetAll().ToList();

            return photos;
        }
    }
}
