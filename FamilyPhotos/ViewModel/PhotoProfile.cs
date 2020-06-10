using AutoMapper;
using FamilyPhotos.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FamilyPhotos.ViewModel
{
    public class PhotoProfile : Profile
    {
        public PhotoProfile()
        {
            CreateMap<PhotoViewModel, PhotoModel>() //Ez áttölti az azonos nevű property-ket.
                .ForMember(dest => dest.ContentTye,
                    options => options.MapFrom(
                        src => src.PictureFromBrowser == null
                            ? null
                            : src.PictureFromBrowser.ContentType))
                .AfterMap((viewModel, model) =>
                    {
                        model.Picture = new byte[viewModel.PictureFromBrowser.Length];

                        //Megnyitjuk és átmásoljuk a feltöltött állomány stream-jét a tömbbe
                        using (var stream = viewModel.PictureFromBrowser.OpenReadStream())
                        {
                            //todo: ehelyett a cast helyett buffer + ciklus
                            stream.Read(model.Picture, 0, (int)viewModel.PictureFromBrowser.Length);
                        }
                    });

            ////Megoldás AutoMapper nélkül:
            ////Készítünk egy fogadó byte tömböt, amibe a kép elfér
            //model.Picture = new byte[viewModel.PictureFromBrowser.Length];

            ////Megnyitjuk és átmásoljuk a feltöltött állomány stream-jét a tömbbe
            //using (var stream = viewModel.PictureFromBrowser.OpenReadStream())
            //{
            //    //todo: ehelyett a cast helyett buffer + ciklus
            //    stream.Read(model.Picture, 0, (int)viewModel.PictureFromBrowser.Length);
            //}

            //model.ContentTye = viewModel.PictureFromBrowser.ContentType;

            CreateMap<PhotoModel, PhotoViewModel>();

            CreateMap<PhotoModel, PhotoModel>(); //Szerintem ez még nemm kell
        }
    }
}
