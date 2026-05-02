using eHotelMartinez.DataAccess.Context;
using eHotelMartinez.Domain.Entities.Attraction;
using eHotelMartinez.Domain.Models.Attraction;
using eHotelMartinez.Domain.Models.Base;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking.Internal;
using eHotelMartinez.Domain.ValueObjects;

namespace eHotelMartinez.BusinessLogic.Core.Attraction
{
    public class AttractionActions
    {
        protected List<AttractionDTO> ExecuteGetAllAttractions()
        {
            using (var db = new AttractionContext())
            {
                return db.Attractions
                    .AsNoTracking()
                    .Include(a => a.Images)
                    .Where(p => p.IsActive == true)
                    .Join(db.Categories,
                    a => a.CategoryId,
                    c => c.Id,
                    (a, c) => new AttractionDTO
                    {
                        Id = a.Id,
                        Name = a.Name,
                        ShortDescription = a.ShortDescription,
                        Description = a.Description,
                        Category = c.Name,
                        Location = a.Location,
                        Distance = a.Distance,
                        Price = a.Price,
                        Rating = a.Rating,
                        Popularity = a.Popularity,
                        Images = a.Images.Select(i => new AttractionImageDTO
                        {
                            Url = i.Url
                        }).ToList(),
                        OpeningHours = a.OpeningHours,
                        Contacts = new PartnerContacts
                        {
                            Phone = a.Contacts.Phone,
                            Email = a.Contacts.Email,
                            BookingUrl = a.Contacts.BookingUrl
                        }
                    }).ToList();
            }
        }

        protected AttractionDTO ExecuteGetAttractionById(int id)
        {
            using (var db = new AttractionContext())
            {
                var a = db.Attractions.FirstOrDefault(a => a.Id == id && a.IsActive == true);
                if (a == null)
                    return null;

                return db.Attractions
                    .AsNoTracking()
                    .Include(a => a.Images)
                    .Where(p => p.IsActive == true)
                    .Join(db.Categories,
                    a => a.CategoryId,
                    c => c.Id,
                    (a, c) => new AttractionDTO
                    {
                        Id = a.Id,
                        Name = a.Name,
                        ShortDescription = a.ShortDescription,
                        Description = a.Description,
                        Category = c.Name,
                        Location = a.Location,
                        Distance = a.Distance,
                        Price = a.Price,
                        Rating = a.Rating,
                        Popularity = a.Popularity,
                        Images = a.Images.Select(i => new AttractionImageDTO
                        {
                            Url = i.Url
                        }).ToList(),
                        OpeningHours = a.OpeningHours,
                        Contacts = new PartnerContacts
                        {
                            Phone = a.Contacts.Phone,
                            Email = a.Contacts.Email,
                            BookingUrl = a.Contacts.BookingUrl
                        }
                    }).First();
            }
        }

        protected ResponseMsg ExecuteCreateAttraction(AttractionData attraction)
        {

            if (!string.IsNullOrWhiteSpace(attraction.Name))
                return new ResponseMsg { IsSuccess = false, Message = "Name can't be empty!" };

            if (CategoryExists(attraction.CategoryId) == false)
                return new ResponseMsg { IsSuccess = false, Message = "Category doesn't exist!" };

            if (attraction.Distance < 0)
                return new ResponseMsg { IsSuccess = false, Message = "Distance can't be negative!" };

            if (attraction.Price < 0)
                return new ResponseMsg { IsSuccess = false, Message = "Price can't be negative!" };

            if (attraction.OpeningHours == null || attraction.OpeningHours.Count == 0)
                return new ResponseMsg { IsSuccess = false, Message = "Opening hours can't be empty!" };

            using (var db = new AttractionContext())
            {
                if (db.Attractions.Any(a => a.Name == attraction.Name && a.IsActive))
                    return new ResponseMsg { IsSuccess = false, Message = "An attraction with the same name already exists!" };
            }

            var newAttraction = new AttractionData
            {
                Name = attraction.Name,
                ShortDescription = attraction.ShortDescription,
                Description = attraction.Description,
                CategoryId = attraction.CategoryId,
                Location = attraction.Location,
                Distance = attraction.Distance,
                Price = attraction.Price,
                Images = attraction.Images.Select(i => new AttractionImageData
                {
                    AttractionId = attraction.Id,
                    Url = i.Url
                }).ToList(),
                OpeningHours = attraction.OpeningHours,
                Contacts = new PartnerContacts
                {
                    Phone = attraction.Contacts.Phone,
                    Email = attraction.Contacts.Email,
                    BookingUrl = attraction.Contacts.BookingUrl
                }
            };

            using (var db = new AttractionContext())
            {
                db.Attractions.Add(attraction);
                db.SaveChanges();
            }
            return new ResponseMsg { IsSuccess = true, Message = "Attraction created successfully." };
        }

        protected ResponseMsg ExecuteUpdateAttraction(UpdateAttractionDTO attraction)
        {
            using (var db = new AttractionContext())
            {
                var existingAttraction = db.Attractions
                    .Include(a => a.Images)
                    .FirstOrDefault(a => a.Id == attraction.Id);

                if (attraction.Distance < 0)
                    return new ResponseMsg { IsSuccess = false, Message = "Distance can't be negative!" };

                if (attraction.Price < 0)
                    return new ResponseMsg { IsSuccess = false, Message = "Price can't be negative!" };

                if (attraction.OpeningHours == null || attraction.OpeningHours.Count == 0)
                    return new ResponseMsg { IsSuccess = false, Message = "Opening hours can't be empty!" };



                if (existingAttraction == null)
                    return new ResponseMsg { IsSuccess = false, Message = "Attraction not found." };

                if (!string.IsNullOrWhiteSpace(attraction.Name))
                    existingAttraction.Name = attraction.Name;

                if (attraction.CategoryId > 0 && attraction.CategoryId != existingAttraction.CategoryId)
                {
                    if (CategoryExists(attraction.CategoryId) == false)
                        return new ResponseMsg { IsSuccess = false, Message = "Category does not exist." };
                    existingAttraction.CategoryId = attraction.CategoryId;
                }

                if (!string.IsNullOrWhiteSpace(attraction.Location))
                    existingAttraction.Location = attraction.Location;

                if (attraction.Distance >= 0)
                    existingAttraction.Distance = attraction.Distance;

                if (attraction.Price >= 0)
                    existingAttraction.Price = attraction.Price;

                if (attraction.OpeningHours != null && attraction.OpeningHours.Count > 0)
                    existingAttraction.OpeningHours = attraction.OpeningHours;


                existingAttraction.ShortDescription = attraction.ShortDescription;
                existingAttraction.Description = attraction.Description;

                foreach (var image in attraction.Images)
                {
                    var exists = existingAttraction.Images
                        .Any(i => i.Url == image.Url && i.IsActive);

                    if (!exists)
                    {
                        existingAttraction.Images.Add(new AttractionImageData
                        {
                            AttractionId = existingAttraction.Id,
                            Url = image.Url,
                        });
                    }
                }

                existingAttraction.Contacts.Phone = attraction.Contacts.Phone;
                existingAttraction.Contacts.Email = attraction.Contacts.Email;
                existingAttraction.Contacts.BookingUrl = attraction.Contacts.BookingUrl;
                db.SaveChanges();
            }
            return new ResponseMsg { IsSuccess = true, Message = "Attraction updated successfully." };
        }

        protected ResponseMsg ExecuteDeleteAttraction(int id)
        {
            using (var db = new AttractionContext())
            {
                var existingAttraction = db.Attractions
                    .Include(a => a.Images)
                    .FirstOrDefault(a => a.Id == id);

                if (existingAttraction == null)
                    return new ResponseMsg { IsSuccess = false, Message = "Attraction not found." };

                existingAttraction.IsActive = false;
                existingAttraction.CategoryId = null;

                foreach (var image in existingAttraction.Images)
                {
                    image.IsActive = false;
                }

                db.SaveChanges();
            }
            return new ResponseMsg { IsSuccess = true, Message = "Attraction deleted successfully." };
        }


        private static bool CategoryExists(int? categoryId)
        {
            using (var db = new AttractionContext())
            {
                return db.Categories.Any(c => c.Id == categoryId && c.IsActive);
            }
        }
    }
}
