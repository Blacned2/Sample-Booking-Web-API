using AutoMapper;
using Dapper;
using KapitalMedyaBooking.AppService.AppServiceModels.Dto;
using KapitalMedyaBooking.AppService.AppServiceModels.Enum;
using KapitalMedyaBooking.AppService.AppServiceModels.Request;
using KapitalMedyaBooking.AppService.AppServiceModels.Response;
using KapitalMedyaBooking.AppService.Interfaces;
using KapitalMedyaBooking.Data.Context;
using KapitalMedyaBooking.Data.Models;
using Microsoft.Extensions.Configuration;
using Npgsql;
using PagedList;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KapitalMedyaBooking.AppService.AppServices
{
    public class UserAppService : BaseAppService, IUserAppService
    {
        private readonly IConfiguration Configuration;
        public UserAppService(KapitalDbContext db, IMapper mapper, IConfiguration configuration) : base(db, mapper)
        {
            this.Configuration = configuration;
        }

        public ServiceResponse<UserDto> CreateOrEdit(UserDto user)
        {
            var serviceResult = new ServiceResponse<UserDto>();

            try
            {
                var userMap = Mapper.Map<User>(user);

                using var con = new NpgsqlConnection(Configuration.GetConnectionString("KapitalMedyaConn"));

                con.Open();

                int lastIndex = con.ExecuteScalar<int>("SELECT id FROM users ORDER BY id DESC LIMIT 1 ;");

                con.Close();

                if (userMap.ID >= 0)
                {
                    DB.Users.Update(userMap);
                    DB.SaveChanges();
                    serviceResult.Message = "Successfully Updated";
                    serviceResult.MessageType = MessageTypeEnum.Success;
                    serviceResult.ReturnObject = user;
                }
                else
                {
                    userMap.ID = lastIndex + 1; //auto increment in here..
                    DB.Users.Add(userMap);
                    DB.SaveChanges();
                    serviceResult.Message = "Successfully Created";
                    serviceResult.MessageType = MessageTypeEnum.Success;
                    serviceResult.ReturnObject = user;
                }
            }
            catch (Exception ex)
            {
                serviceResult.Message = ex.Message;
                serviceResult.MessageType = MessageTypeEnum.Error;
            }
            return serviceResult;
        }

        public ServiceResponse<bool> Delete(int userID)
        {
            var serviceResult = new ServiceResponse<bool>();

            try
            {
                var user = DB.Users.Find(userID);

                if (user != null)
                {
                    DB.Remove(user);
                    DB.SaveChanges();
                    serviceResult.Message = string.Empty;
                    serviceResult.MessageType = MessageTypeEnum.Success;
                    serviceResult.ReturnObject = true;
                }
                else
                {
                    serviceResult.Message = "User not found";
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

        public ServiceResponse<UserDto> Get(int userID)
        {
            var serviceResult = new ServiceResponse<UserDto>();

            try
            {
                serviceResult.ReturnObject = Mapper
                                                .Map<UserDto>
                                                    (DB.Users
                                                        .Find(userID));
            }
            catch (Exception ex)
            {
                serviceResult.Message = ex.Message;
            }
            return serviceResult;
        }

        public ServiceResponse<IPagedList<UserListDto>> Search(UserSearchRequest request)
        {
            var serviceResult = new ServiceResponse<IPagedList<UserListDto>>();

            try
            {
                var results = (from u in DB.Users
                               where
                               1 == 1
                               && (string.IsNullOrEmpty(request.Email) || u.Email == request.Email)
                               && (string.IsNullOrEmpty(request.FirstName) || u.FirstName.Contains(request.FirstName))
                               && (string.IsNullOrEmpty(request.LastName) || u.LastName.Contains(request.LastName))
                               && (string.IsNullOrEmpty(request.Phone) || u.Phone == request.Phone)
                               select new UserListDto
                               {
                                   ID = u.ID,
                                   City = u.City,
                                   Country = u.Country,
                                   Email = u.Email,
                                   FirstName = u.FirstName,
                                   LastName = u.LastName,
                                   FullName = u.FullName,
                                   Phone = u.Phone,
                                   Image = u.Image,
                                   JobTitle = u.JobTitle,
                                   JobType = u.JobType,
                                   OnboardingCompletion = u.OnboardingCompletion
                               }).ToPagedList(request.Page, request.PageSize);

                if (results.Any())
                {
                    serviceResult.Message = string.Empty;
                    serviceResult.MessageType = MessageTypeEnum.Success;
                    serviceResult.ReturnObject = results;
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
