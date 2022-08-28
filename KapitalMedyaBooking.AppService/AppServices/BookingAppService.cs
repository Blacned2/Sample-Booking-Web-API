using AutoMapper;
using KapitalMedyaBooking.AppService.AppServiceModels.Dto;
using KapitalMedyaBooking.AppService.AppServiceModels.Enum;
using KapitalMedyaBooking.AppService.AppServiceModels.Request;
using KapitalMedyaBooking.AppService.AppServiceModels.Response;
using KapitalMedyaBooking.AppService.Interfaces;
using KapitalMedyaBooking.Data.Context;
using KapitalMedyaBooking.Data.Models;
using System.Linq;
using PagedList;
using System;
using KapitalMedyaBooking.Core;
using Microsoft.Extensions.Configuration;
using Microsoft.Data.SqlClient;
using Dapper;
using Npgsql;

namespace KapitalMedyaBooking.AppService.AppServices
{
    public class BookingAppService : BaseAppService, IBookingAppService
    {
        private readonly IConfiguration Configuration;
        public BookingAppService(KapitalDbContext db, IMapper mapper, IConfiguration configuration) : base(db, mapper)
        {
            this.Configuration = configuration;
        }

        public ServiceResponse<BookingDto> CreateOrEdit(BookingDto booking)
        {
            var serviceResult = new ServiceResponse<BookingDto>();

            try
            {
                var bookingMap = Mapper.Map<Booking>(booking);

                using var con = new NpgsqlConnection(Configuration.GetConnectionString("KapitalMedyaConn"));

                con.Open();

                int lastIndex = con.ExecuteScalar<int>("SELECT id FROM bookings ORDER BY id DESC LIMIT 1 ;");

                con.Close();

                if (bookingMap.ID >= 0)
                {
                    DB.Bookings.Update(bookingMap);
                    DB.SaveChanges();
                    serviceResult.Message = "Successfully Updated";
                    serviceResult.MessageType = MessageTypeEnum.Success;
                    serviceResult.ReturnObject = booking;
                }
                else
                {
                    bookingMap.ID = lastIndex + 1; //auto increment in here..
                    DB.Bookings.Add(bookingMap);
                    DB.SaveChanges();
                    serviceResult.Message = "Successfully Created";
                    serviceResult.MessageType = MessageTypeEnum.Success;
                    serviceResult.ReturnObject = booking;
                }
            }
            catch (Exception ex)
            {
                serviceResult.Message = ex.Message;
                serviceResult.MessageType = MessageTypeEnum.Error;
            }
            return serviceResult;
        }

        public ServiceResponse<bool> Delete(int bookingID)
        {
            var serviceResult = new ServiceResponse<bool>();

            try
            {
                var booking = DB.Bookings.Find(bookingID);

                if (booking != null && booking.Confirmed != 1)
                {
                    DB.Remove(booking);
                    DB.SaveChanges();
                    serviceResult.MessageType = MessageTypeEnum.Success;
                    serviceResult.ReturnObject = true;
                }
                else
                {
                    serviceResult.Message = "The Booking is either not found or confirmed";
                    serviceResult.MessageType = MessageTypeEnum.Warning;
                    serviceResult.ReturnObject = false;
                }
            }
            catch (Exception ex)
            {
                serviceResult.Message = ex.Message;
                serviceResult.MessageType = MessageTypeEnum.Error;
                serviceResult.ReturnObject = false;
            }
            return serviceResult;
        }

        public ServiceResponse<BookingDto> Get(int bookingID)
        {
            var serviceResult = new ServiceResponse<BookingDto>();

            try
            {
                serviceResult.ReturnObject = Mapper
                                                .Map<BookingDto>
                                                    (DB.Bookings
                                                        .Find(bookingID));

                if(serviceResult.ReturnObject == null)
                {
                    serviceResult.MessageType = MessageTypeEnum.Warning;
                    throw new Exception("Not Found!");
                }
            }
            catch (Exception ex)
            {
                serviceResult.Message = ex.Message;
            }
            return serviceResult;
        }

        public ServiceResponse<IPagedList<BookingListDto>> Search(BookingSearchRequest request)
        {
            var serviceResult = new ServiceResponse<IPagedList<BookingListDto>>();

            try
            {
                var data = (from b in DB.Bookings
                               join a in DB.Appartments on b.ApartmentID equals a.ID
                               join u in DB.Users on b.UserID equals u.ID
                               where
                               1 == 1
                               && (string.IsNullOrEmpty(request.FirstName) || u.FirstName.Contains(request.FirstName))
                               && (string.IsNullOrEmpty(request.LastName) || u.LastName.Contains(request.LastName))
                               && (string.IsNullOrEmpty(request.Email) || u.Email.Contains(request.Email))
                               && (string.IsNullOrEmpty(request.Phone) || u.Phone.Contains(request.Phone))
                               && (string.IsNullOrEmpty(request.AppartmentName) || a.Name.Contains(request.AppartmentName))
                               && (request.Confirmed == null || b.Confirmed == request.Confirmed)
                               && (string.IsNullOrEmpty(request.Address) || a.Address.Contains(request.Address))
                               && (string.IsNullOrEmpty(request.Address) || a.Address2.Contains(request.Address))
                               && (string.IsNullOrEmpty(request.ZipCode) || a.ZipCode == request.ZipCode)
                               && (string.IsNullOrEmpty(request.City) || a.City.Contains(request.City))
                               && (string.IsNullOrEmpty(request.Country) || a.City.Contains(request.Country))
                               select new BookingListDto() //TODO
                               {
                                   ApartmentName = a.Name,
                                   City = a.City,
                                   Country = a.Country,
                                   FirstName = u.FirstName,
                                   LastName = u.LastName,
                                   Email = u.Email,
                                   Phone = u.Phone,
                                   BookedAt = b.BookedAt.ToDateTime(),
                                   BookedFor = b.BookedFor,
                                   Confirmed = b.Confirmed,
                                   ID = b.ID,
                                   AppartmentAddress = a.Address + " | " + a.Address2,
                                   AppartmentZipCode = a.ZipCode,
                                   StartsAt = b.StartsAt.ToDateTime(),
                                   EndDate = b.StartsAt.ToDateTime().AddDays(b.BookedFor)
                               }).OrderBy(o => o.ID).ToList();

                var filteredResults = data.Where(u => request.DateTimeInterval == null || (u.StartsAt <= request.DateTimeInterval && u.StartsAt.AddDays(u.BookedFor) >= request.DateTimeInterval)).ToPagedList(request.Page,request.PageSize); //I refiltered the data because our data type in db is string.

                if (filteredResults.Any())
                {
                    serviceResult.Message = string.Empty;
                    serviceResult.MessageType = MessageTypeEnum.Success;
                    serviceResult.ReturnObject = filteredResults;
                }
                else
                {
                    serviceResult.Message = "Not Found!";
                    serviceResult.MessageType = MessageTypeEnum.Warning;
                }

            }
            catch (Exception ex)
            {
                serviceResult.Message = ex.Message;
                serviceResult.MessageType = MessageTypeEnum.Error;
            }

            return serviceResult;
        }
    }
}
