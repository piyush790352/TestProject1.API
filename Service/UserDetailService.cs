using DemoProject1.API.Model.Domain;
using System.Linq;
using System.Runtime.Intrinsics.X86;
using System.Text.Json;
using TestProject1.API.IService;
using TestProject1.API.Model.Domain;
using TestProject1.API.Model.DTO;
using TestProject1.API.Repository;
using static TestProject1.API.Model.Domain.ScoreSheet;
using static TestProject1.API.Model.DTO.AddMarksheetDetailDTO;

namespace TestProject1.API.Service
{
    public class UserDetailService : IUserDetailService
    {
        private readonly ISchoolRepository<UserDetail> _userDetailRepository;
        private readonly ISchoolRepository<User> _userRepository;
        private readonly ISchoolRepository<Subject> _subjectRepository;
        private readonly ISchoolRepository<Grade> _gradeRepository;
        private readonly ISchoolRepository<ScoreSheet> _scoresheetRepository;
        public UserDetailService(ISchoolRepository<UserDetail> userDetailRepository, ISchoolRepository<User> userRepository,
            ISchoolRepository<Subject> subjectRepository, ISchoolRepository<Grade> gradeRepository, ISchoolRepository<ScoreSheet> scoresheetRepository)
        {
            _userDetailRepository = userDetailRepository;
            _userRepository = userRepository;
            _subjectRepository = subjectRepository;
            _gradeRepository = gradeRepository;
            _scoresheetRepository = scoresheetRepository;
        }

        string path1 = null;
        string path2 = null;
        string subjectPath = null;
        string gradePath = null;
        string scoresheetPath = null;
        public UserDetailService()
        {
            path1 = @".\JsonData\UserList.json";
            path2 = @".\JsonData\UserDetailList.json";
            subjectPath = @".\JsonData\Subject.json";
            gradePath = @".\JsonData\Grade.json";
            scoresheetPath = @".\JsonData\ScoreSheet.json";
        }

        public async Task<Response<List<UserDetailDTO>>> GetUserDetails()
        {
            try
            {
                UserDetailService userDetailService = new UserDetailService();
                var responseUserList = _userRepository.Get(userDetailService.path1);
                if (responseUserList == null)
                {
                    return new Response<List<UserDetailDTO>>
                    {
                        StatusMessage = "No Record Found!."
                    };
                }
                else
                {
                    var responseUserDetail = _userDetailRepository.Get(userDetailService.path2);
                    var result = (from objuser in responseUserList
                                  join objuserDetail in responseUserDetail on objuser.UserId equals objuserDetail.UserId
                                  select new UserDetailDTO()
                                  {
                                      UserId = objuser.UserId,
                                      UserName = objuser.UserName,
                                      FirstName = objuserDetail.FirstName,
                                      LastName = objuserDetail.LastName,
                                      Email = objuserDetail.Email,
                                      Gender = objuserDetail.Gender,
                                      Specialization = objuserDetail.Specialization,
                                      IsEmployee = objuserDetail.IsEmployee
                                  }).ToList();
                    if (result.Count > 0)
                    {
                        return new Response<List<UserDetailDTO>>
                        {
                            Result = result,
                            StatusMessage = "Ok"
                        };
                    }
                    else
                    {
                        return new Response<List<UserDetailDTO>>
                        {
                            StatusMessage = "No recored found!."
                        };
                    }

                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<Response<UserDetailDTO>> GetUserDetailById(int Id)
        {
            try
            {
                UserDetailService userDetailService = new UserDetailService();
                var responseUserList = _userRepository.Get(userDetailService.path1);

                if (responseUserList == null)
                {
                    return new Response<UserDetailDTO>
                    {
                        StatusMessage = "No Record Found!."
                    };
                }
                else
                {
                    var responseUserDetail = _userDetailRepository.Get(userDetailService.path2);
                    var result = (from objuser in responseUserList
                                  join objuserDetail in responseUserDetail on objuser.UserId equals objuserDetail.UserId
                                  where objuserDetail.UserId == Id
                                  select new UserDetailDTO()
                                  {
                                      UserName = objuser.UserName,
                                      FirstName = objuserDetail.FirstName,
                                      LastName = objuserDetail.LastName,
                                      Email = objuserDetail.Email,
                                      Gender = objuserDetail.Gender,
                                      Specialization = objuserDetail.Specialization,
                                      IsEmployee = objuserDetail.IsEmployee
                                  }).FirstOrDefault();
                    if (result != null)
                    {
                        return new Response<UserDetailDTO>
                        {
                            Result = result,
                            StatusMessage = "Ok"
                        };
                    }
                    else
                    {
                        return new Response<UserDetailDTO>
                        {
                            StatusMessage = "No recored found!."
                        };
                    }
                }

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<Response<AddUserDetailDTO>> AddUserDetail(AddUserDetailDTO addUserDetailRequestDTO)
        {
            try
            {
                UserDetailService userDetailService = new UserDetailService();
                var responseUserList = _userRepository.Get(userDetailService.path1);

                var usercheck = (from obj in responseUserList
                                 where obj.UserName.Equals(addUserDetailRequestDTO.UserName) &&
                                 obj.Password.Equals(addUserDetailRequestDTO.Password)
                                 select obj).Count();


                if (usercheck > 0)
                {
                    return new Response<AddUserDetailDTO>
                    {

                        StatusMessage = "User already exists"
                    };
                }
                else
                {

                    int UserId = responseUserList.Count > 0 ? responseUserList[responseUserList.Count - 1].UserId + 1 : 1;
                    var user = new User()
                    {
                        UserId = UserId,
                        UserName = addUserDetailRequestDTO.UserName,
                        Password = addUserDetailRequestDTO.Password,

                    };
                    responseUserList.Add(user);
                    _userRepository.Set(userDetailService.path1, responseUserList);


                    var responseUserDetail = _userDetailRepository.Get(userDetailService.path2);

                    var userDetail = new UserDetail()
                    {
                        Id = UserId,
                        FirstName = addUserDetailRequestDTO.FirstName,
                        LastName = addUserDetailRequestDTO.LastName,
                        Gender = addUserDetailRequestDTO.Gender,
                        Email = addUserDetailRequestDTO.Email,
                        Specialization = addUserDetailRequestDTO.Specialization,
                        IsEmployee = addUserDetailRequestDTO.IsEmployee,
                        UserId = UserId,
                    };

                    if (userDetail != null)
                    {
                        responseUserDetail.Add(userDetail);
                        _userDetailRepository.Set(userDetailService.path2, responseUserDetail);


                        return new Response<AddUserDetailDTO>
                        {
                            Result = addUserDetailRequestDTO,
                            StatusMessage = "Data has been added successfully!."
                        };
                    }
                    else
                    {
                        return new Response<AddUserDetailDTO>
                        {
                            StatusMessage = "No Record found..!"
                        };
                    }
                }

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }


        public async Task<Response<string>> AddMarksheetDetail(AddMarksheetDetailDTO addMarksheetDetailDTO)
        {
            try
            {
                UserDetailService userDetailService = new UserDetailService();
                var responseSubjectList = _subjectRepository.Get(userDetailService.subjectPath);
                var responseScoreSheetList = _scoresheetRepository.Get(userDetailService.scoresheetPath);
                var subjectCheck = 0;
                var resUser = responseScoreSheetList.FirstOrDefault(x => x.UserId == addMarksheetDetailDTO.UserId);
                if (resUser == null)
                {
                    foreach (var item in addMarksheetDetailDTO.markSheetListNew)
                    {
                        subjectCheck = (from obj in responseSubjectList
                                        where obj.SubjectId.Equals(item.subjectId)
                                        //&& scoreSheet1.UserId.Equals(addMarksheetDetailDTO.UserId)
                                        select obj).Count();

                        if (subjectCheck == 0)
                        {

                            List<ScoreSheet.MarkSheetList> markSheetLists = new List<ScoreSheet.MarkSheetList>();
                            ScoreSheet.MarkSheetList markSheet = new ScoreSheet.MarkSheetList();
                            markSheet.subjectId = item.subjectId;
                            markSheet.gradeId = item.gradeId;
                            markSheetLists.Add(markSheet);
                            ScoreSheet scoreSheet = new ScoreSheet()
                            {
                                UserId = addMarksheetDetailDTO.UserId,
                                markSheetList = markSheetLists
                            };
                            responseScoreSheetList.Add(scoreSheet);
                            _scoresheetRepository.Set(userDetailService.scoresheetPath, responseScoreSheetList);


                            var subject = new Subject()
                            {
                                SubjectId = item.subjectId,
                                SubjectName = item.subject,
                                //SubjectDescription = item.SubjectDescription,

                            };
                            responseSubjectList.Add(subject);
                            _subjectRepository.Set(userDetailService.subjectPath, responseSubjectList);


                            var responseGradeDetail = _gradeRepository.Get(userDetailService.gradePath);

                            var grade = new Grade()
                            {
                                GradeId = item.gradeId,
                                GradeType = item.grade,
                                //GradeDescription = item.GradeDescription,
                                //SubjectId = Id
                            };

                            responseGradeDetail.Add(grade);
                            _gradeRepository.Set(userDetailService.gradePath, responseGradeDetail);

                        }
                    }
                    if (subjectCheck > 0)
                    {
                        return new Response<string>
                        {
                            StatusMessage = "Subject already added for this students."
                        };
                    }
                    return new Response<string>
                    {
                        StatusMessage = "Data added successfully!.."
                    };
                }
                else
                {
                    foreach (var item in addMarksheetDetailDTO.markSheetListNew)
                    {
                        subjectCheck = (from obj in responseSubjectList
                                        where obj.SubjectId.Equals(item.subjectId)
                                        //&& resUser.UserId.Equals(addMarksheetDetailDTO.UserId)
                                        select obj).Count();

                        if (subjectCheck == 0)
                        {

                            List<ScoreSheet.MarkSheetList> markSheetLists = new List<ScoreSheet.MarkSheetList>();
                            ScoreSheet.MarkSheetList markSheet = new ScoreSheet.MarkSheetList();
                            markSheet.subjectId = item.subjectId;
                            markSheet.gradeId = item.gradeId;
                            markSheetLists.Add(markSheet);
                            //ScoreSheet scoreSheet = new ScoreSheet()
                            //{
                            //    //UserId = addMarksheetDetailDTO.UserId,
                            //    markSheetList = markSheetLists
                            //};
                            //responseScoreSheetList.Add(markSheetLists);
                            resUser.markSheetList.Add(markSheet);
                            _scoresheetRepository.Set(userDetailService.scoresheetPath, responseScoreSheetList);


                            var subject = new Subject()
                            {
                                SubjectId = item.subjectId,
                                SubjectName = item.subject,
                                //SubjectDescription = item.SubjectDescription,

                            };
                            responseSubjectList.Add(subject);
                            _subjectRepository.Set(userDetailService.subjectPath, responseSubjectList);


                            var responseGradeDetail = _gradeRepository.Get(userDetailService.gradePath);

                            var grade = new Grade()
                            {
                                GradeId = item.gradeId,
                                GradeType = item.grade,
                                //GradeDescription = item.GradeDescription,
                                //SubjectId = Id
                            };

                            responseGradeDetail.Add(grade);
                            _gradeRepository.Set(userDetailService.gradePath, responseGradeDetail);

                        }
                    }
                    if (subjectCheck > 0)
                    {
                        return new Response<string>
                        {
                            StatusMessage = "Subject already added for this students."
                        };
                    }
                    return new Response<string>
                    {
                        StatusMessage = "Data added successfully!.."
                    };
                }

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }


        public async Task<Response<List<GetMarkSheetDetailDTO>>> GetMarksheetList(int UserId)
        {
            try
            {
                UserDetailService userDetailService = new UserDetailService();
                var responseSubjectList = _subjectRepository.Get(userDetailService.subjectPath);
                var responseGradeList = _gradeRepository.Get(userDetailService.gradePath);
                var responseScoreSheet = _scoresheetRepository.Get(userDetailService.scoresheetPath);



                if (responseSubjectList == null)
                {
                    return new Response<List<GetMarkSheetDetailDTO>>
                    {
                        StatusMessage = "No Record Found!."
                    };
                }
                else
                {
                    List<GetMarkSheetDetailDTO> getMarkSheetDetailDTO = new List<GetMarkSheetDetailDTO>();
                    var resUser = (from objScoreSheet in responseScoreSheet
                                   where objScoreSheet.UserId == UserId
                                   select objScoreSheet).FirstOrDefault();
                    foreach (var item in resUser.markSheetList)
                    {
                       var result = (from objSubject in responseSubjectList
                                      join objScoreSheet in responseScoreSheet on objSubject.SubjectId equals item.subjectId
                                      join objGrade in responseGradeList on item.gradeId equals objGrade.GradeId
                                      select new GetMarkSheetDetailDTO()
                                      {
                                          subject = objSubject.SubjectName,
                                          grade = objGrade.GradeType
                                      }).FirstOrDefault();
                        if (result == null)
                        {
                            return new Response<List<GetMarkSheetDetailDTO>>
                            {
                                StatusMessage = "No recored found!."
                            };
                        }
                        getMarkSheetDetailDTO.Add(result);
                    }
                    return new Response<List<GetMarkSheetDetailDTO>>
                    {
                        Result = getMarkSheetDetailDTO,
                        StatusMessage = "Ok"
                    };
                }
                //return new Response<List<GetMarkSheetDetailDTO>>
                //{
                //    //Result = result,
                //    StatusMessage = "No recored found!."
                //};
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
