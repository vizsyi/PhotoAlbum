using AutoMapper;
using FamilyPhotos.Models;
using FamilyPhotos.Repository;
using FamilyPhotos.ViewModel;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FamilyPhotos.Controllers
{
    public class PhotoController:Controller
    {
        private readonly PhotoRepository repository;
        private readonly IMapper mapper;

        public PhotoController(PhotoRepository repository, IMapper mapper)
        {
            this.repository = repository ?? throw new ArgumentNullException(nameof(repository));
            this.mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
        }
        
        public IActionResult Index()
        {
            var pics = repository.GetAllPhotos();
                        
            return View(pics);
        }

        public IActionResult Details(int id)
        {
            //var model = repository.Get(id);
            var model = repository.GetPicture(id);

            var viewModel = mapper.Map<PhotoViewModel>(model);

            return View(viewModel);
        }

        [HttpGet]
        public IActionResult Edit(int id)
        {
            var model = repository.GetPicture(id);

            var viewModel = mapper.Map<PhotoViewModel>(model);

            return View(viewModel);
        }

        [HttpPost]
        public IActionResult Edit(PhotoViewModel viewModel)
        {

            if (!ModelState.IsValid)
            {
                return View(viewModel);
            }

            var model = mapper.Map<PhotoModel>(viewModel);

            repository.UpdatePhoto(model);

            return RedirectToAction("Index");
        }

        public FileContentResult GetImage(int photoID)
        {
            var pic = repository.GetPicture(photoID: photoID); //todo: pic lehet null
            if (pic == null || pic.Picture == null)
            {
                return null;
            }

            return File(pic.Picture, pic.ContentTye);
        }

        [HttpGet]
        public IActionResult Create()
        {
            return View(new PhotoViewModel());
        }

        [HttpPost]
        public IActionResult Create(PhotoViewModel viewModel)
        {

            if (!ModelState.IsValid)
            {
                return View(viewModel);
            }

            //több profile betöltése
            //var automapperCgf = new AutoMapper.MapperConfiguration(
            //    cfg =>
            //    {
            //        cfg.AddProfile(new PhotoProfile()));
            //        cfg.AddProfile(new PhotoProfile()));
            //        cfg.AddProfile(new PhotoProfile()));
            //    });

            var model = mapper.Map<PhotoModel>(viewModel);
            
            repository.addPhoto(model);

            return RedirectToAction("Index");
        }

        [HttpGet]
        public IActionResult Delete(int id)
        {
            var model = repository.GetPicture(id);
            var viewModel = mapper.Map<PhotoViewModel>(model);
            return View(viewModel);
        }

        [HttpPost]
        public IActionResult Delete(PhotoViewModel viewModel)
        {
            repository.DeletePhoto(viewModel.Id);

            return RedirectToAction("Index");
        }

        //10. 1:12:25 + 3.megoldás
    }
}
