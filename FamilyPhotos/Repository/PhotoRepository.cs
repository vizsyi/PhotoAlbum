using FamilyPhotos.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FamilyPhotos.Repository
{
    public class PhotoRepository
    {
        //private List<PhotoModel> data = new List<PhotoModel> { new PhotoModel { Id=1, Title = "Egy kép" } };
        private List<PhotoModel> data = new List<PhotoModel> ();
        int id = 0;

        public IEnumerable<PhotoModel> GetAllPhotos()
        {
            return data;
        }

        public PhotoModel GetPicture(int photoID)
        {
            return data.SingleOrDefault(x => x.Id == photoID);
        }

        internal void addPhoto(PhotoModel model)
        {
            model.Id = id++;
            data.Add(model);
        }

        internal object Get()
        {
            throw new NotImplementedException();
        }

        public void UpdatePhoto(PhotoModel model)
        {
            var oldModel = data.SingleOrDefault(x => x.Id == model.Id);
            if (oldModel != null)
            {
                data.Remove(oldModel);
                data.Add(model);
            }
        }

        public void DeletePhoto(int id)
        {
            var oldModel = data.SingleOrDefault(x => x.Id == id);
            if (oldModel != null)
            {
                data.Remove(oldModel);
            }
        }
    }
}
