using eHotelMartinez.BusinessLogic.Helpers;
using eHotelMartinez.DataAccess.Context;
using eHotelMartinez.Domain.Entities.Attraction;
using eHotelMartinez.Domain.Models.Attraction;
using eHotelMartinez.Domain.Models.Base;
using eHotelMartinez.Domain.ValueObjects;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking.Internal;
using System.Linq;

namespace eHotelMartinez.BusinessLogic.Core.Attraction
{
    public class AttractionActions
    {
        protected List<AttractionDTO> ExecuteGetAllAttractions()
        {
            using var db = new CategoryContext();

            var categories = db.Categories
                .AsNoTracking()
                .Where(c => c.IsActive)
                .ToDictionary(c => c.Id, c => c.Name);

            var attractions = db.Attractions
                .AsNoTracking()
                .Include(a => a.Images)
                .Where(p => p.IsActive == true)
                .ToList();

            return attractions.Select(a => new AttractionDTO
                {
                    Id = a.Id,
                    Name = a.Name,
                    ShortDescription = a.ShortDescription,
                    Description = a.Description,
                    Category = a.CategoryId.HasValue && categories.TryGetValue(a.CategoryId.Value, out var categoryName) ? categoryName : null,
                    Location = a.Location,
                    Distance = a.Distance,
                    Price = a.Price,
                    Rating = a.Rating,
                    Popularity = a.Popularity,
                    Images = a.Images.Select(i => new AttractionImageDTO
                    {
                        Url = i.Url
                    }).ToList(),
                    OpeningHours = a.OpeningHours.Select(oh => new OpeningHourDTO
                    {
                        DayOfWeek = oh.DayOfWeek,
                        Start = oh.Start,
                        End = oh.End
                    }).ToList(),

                    Contacts = new PartnerContacts
                    {
                        Phone = a.Contacts.Phone,
                        Email = a.Contacts.Email,
                        BookingUrl = a.Contacts.BookingUrl
                    }
                }).ToList();
        }

        protected AttractionDTO ExecuteGetAttractionById(int id)
        {
            using var db = new CategoryContext();

            var categories = db.Categories
                 .AsNoTracking()
                 .Where(c => c.IsActive)
                 .ToDictionary(c => c.Id, c => c.Name);

            var a = db.Attractions
                .AsNoTracking()
                .Include(a => a.Images)
                .FirstOrDefault(a => a.Id == id && a.IsActive == true);

            if (a == null)
                return null;

            return new AttractionDTO
            {
                Id = a.Id,
                Name = a.Name,
                ShortDescription = a.ShortDescription,
                Description = a.Description,
                Category = a.CategoryId.HasValue && categories.TryGetValue(a.CategoryId.Value, out var categoryName) ? categoryName : null,
                Location = a.Location,
                Distance = a.Distance,
                Price = a.Price,
                Rating = a.Rating,
                Popularity = a.Popularity,
                Images = a.Images.Select(i => new AttractionImageDTO
                {
                    Url = i.Url
                }).ToList(),
                OpeningHours = a.OpeningHours.Select(oh => new OpeningHourDTO
                {
                    DayOfWeek = oh.DayOfWeek,
                    Start = oh.Start,
                    End = oh.End
                }).ToList(),

                Contacts = new PartnerContacts
                {
                    Phone = a.Contacts.Phone,
                    Email = a.Contacts.Email,
                    BookingUrl = a.Contacts.BookingUrl
                }
            };
        }

        protected ResponseAction ExecuteCreateAttraction(CreateAttractionDTO attraction)
        {

            if (string.IsNullOrWhiteSpace(attraction.Name))
                return new ResponseAction { IsSuccess = false, Message = "Name can't be empty!" };

            if (CategoryCheck.CategoryExists(attraction.CategoryId) == false)
                return new ResponseAction { IsSuccess = false, Message = "Category doesn't exist!" };

            if (attraction.Distance < 0)
                return new ResponseAction { IsSuccess = false, Message = "Distance can't be negative!" };

            if (attraction.Price < 0)
                return new ResponseAction { IsSuccess = false, Message = "Price can't be negative!" };

            if (attraction.OpeningHours == null || attraction.OpeningHours.Count == 0)
                return new ResponseAction { IsSuccess = false, Message = "Opening hours can't be empty!" };

            using (var db = new CategoryContext())
            {
                var existAttraction = db.Attractions.FirstOrDefault(a => a.Name == attraction.Name && a.IsActive);
                if (existAttraction != null)
                {
                    return new ResponseAction
                    {
                        IsSuccess = false,
                        Message = "An attraction with the same name already exists!",
                        Id = existAttraction.Id
                    };
                }
                 
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
                Images  = attraction.Images.Select(i => new AttractionImageData
                {
                    Url = i.Url
                }).ToList(),

                OpeningHours = attraction.OpeningHours.Select(oh => new OpeningHourData
                {
                    DayOfWeek = oh.DayOfWeek,
                    Start = oh.Start,
                    End = oh.End
                }).ToList(),

                Contacts = new PartnerContacts
                {
                    Phone = attraction.Contacts.Phone,
                    Email = attraction.Contacts.Email,
                    BookingUrl = attraction.Contacts.BookingUrl
                }
            };

            using (var db = new CategoryContext())
            {
                db.Attractions.Add(newAttraction);
                db.SaveChanges();
            }
            return new ResponseAction { IsSuccess = true, Message = "Attraction created successfully.", Id = newAttraction.Id};
        }

        protected ResponseMsg ExecuteUpdateAttraction(UpdateAttractionDTO attraction)
        {
            using (var db = new CategoryContext())
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
                    if (CategoryCheck.CategoryExists(attraction.CategoryId) == false)
                        return new ResponseMsg { IsSuccess = false, Message = "Category does not exist." };
                    existingAttraction.CategoryId = attraction.CategoryId;
                }

                if (!string.IsNullOrWhiteSpace(attraction.Location))
                    existingAttraction.Location = attraction.Location;

                if (attraction.Distance >= 0)
                    existingAttraction.Distance = attraction.Distance;

                if (attraction.Price >= 0)
                    existingAttraction.Price = attraction.Price;

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
                            Url = image.Url,
                        });
                    }
                }

                foreach (var openingHour in attraction.OpeningHours)
                {
                    var exists = existingAttraction.OpeningHours
                        .Any(o => o.DayOfWeek == openingHour.DayOfWeek && o.Start == openingHour.Start && o.End == openingHour.End);

                    if (!exists)
                    {
                        existingAttraction.OpeningHours.Add(new OpeningHourData
                        {
                            DayOfWeek = openingHour.DayOfWeek,
                            Start = openingHour.Start,
                            End = openingHour.End
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
            using (var db = new CategoryContext())
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

                foreach (var openingHour in existingAttraction.OpeningHours)
                {
                    openingHour.IsActive = false;
                }

                db.SaveChanges();
            }
            return new ResponseMsg { IsSuccess = true, Message = "Attraction deleted successfully." };
        }
    }
}
