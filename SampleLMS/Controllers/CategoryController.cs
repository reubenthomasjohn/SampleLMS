﻿using Microsoft.AspNetCore.Mvc;
using SampleLMS.Dal.Interfaces;
using SampleLMS.Models.DomainModels;
using SampleLMS.Models.DTOs.Category;

namespace SampleLMS.Controllers
{
    public class CategoryController : Controller
    {
        private readonly ICategoryInterface _categoryRepository;
        public CategoryController(ICategoryInterface categoryRepository)
        {
            _categoryRepository = categoryRepository;
        }

        [HttpGet]
        public IActionResult Add()
        {
            return View();
        }

        [HttpPost]
        [ActionName("Add")]
        public async Task<IActionResult> Add(AddCategoryRequest addCategoryRequest)
        {
            //ValidateAddTagRequest(addCategoryRequest);

            if (ModelState.IsValid == false)
            {
                return View();
            }
            var tag = new Category
            {
                Name = addCategoryRequest.Name,
                //DisplayName = addTagRequest.DisplayName,
            };

            await _categoryRepository.AddAsync(tag);

            return RedirectToAction("List");
        }

        [HttpGet]
        public async Task<IActionResult> List()
        {
            // use dbContext to read the tags
            var tags = await _categoryRepository.GetAllAsync();
            return View(tags);
        }

        [HttpGet]
        public async Task<IActionResult> Edit(int id)
        {
            var category = await _categoryRepository.GetAsync(id);

            if (category != null)
            {
                var editCatergoryRequest = new EditCategoryRequest
                {
                    Id = category.CategoryId,
                    //DisplayName = category.DisplayName,
                    Name = category.Name,
                };
                return View(editCatergoryRequest);
            }
            return View(null);
        }


        [HttpPost]
        public async Task<IActionResult> Edit(EditCategoryRequest editCategoryRequest)
        {
            var category = new Category
            {
                CategoryId = editCategoryRequest.Id,
                Name = editCategoryRequest.Name,
                //DisplayName = editTagRequest.DisplayName,
            };

            var updatedCategory = await _categoryRepository.UpdateAsync(category);
            if (updatedCategory != null)
            {
				// show success notif
				return RedirectToAction("List");
			}
            else
            {
                // show error notif
            }
            return RedirectToAction("Edit", new { id = editCategoryRequest.Id });
        }

        [HttpPost]
        public async Task<IActionResult> DeleteFromUpdatePage(EditCategoryRequest editCategoryRequest)
        {
            var deletedCategory = await _categoryRepository.DeleteAsync(editCategoryRequest.Id);

            if (deletedCategory != null)
            {
                // show success notif
                return RedirectToAction("List");
            }
            else
            {
                //show error notification
                return RedirectToAction("Edit", new { id = editCategoryRequest.Id });
            }
        }

		[HttpGet]
		public async Task<IActionResult> DeleteFromListPage(int id)
		{
            var existingCategory = await _categoryRepository.GetAsync(id);
            if (existingCategory != null)
            {
				existingCategory = await _categoryRepository.DeleteAsync(id);
				return RedirectToAction("List");
			}

			else
			{
				//show error notification
				return RedirectToAction("List");
			}
		}

		//private void ValidateAddCategoryRequest(AddCategoryRequest addCategoryRequest)
		//{
		//    if (addCategoryRequest.Name is not null)
		//    {
		//        if (addCategoryRequest.Name == addCategoryRequest.Name)
		//        {
		//            ModelState.AddModelError("Name", "The Name cannot be the same as _Name");
		//        }
		//    }
		//}
	}
}

