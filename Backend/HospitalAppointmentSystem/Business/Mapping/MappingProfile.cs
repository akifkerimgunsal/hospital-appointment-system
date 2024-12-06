using AutoMapper;
using Entities.Concrete;
using DTO.AdminDTOs;
using DTO.AppointmentDTOs;
using DTO.DoctorDTOs;
using DTO.FeedbackDTOs;
using DTO.LiveSupportDTOs;
using DTO.MedicalReportDTOs;
using DTO.UserDTOs;
using Entities.Enum;

namespace Business.Mapping
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            // Admin Mapping
            CreateMap<AdminAppointmentDTO, AppointmentDetailsDTO>().ReverseMap();
            CreateMap<AdminUserDTO, UserProfileDTO>().ReverseMap();

            // Appointment Mapping
            CreateMap<Appointment, AppointmentDetailsDTO>()
                .ForMember(dest => dest.Doctor, opt => opt.MapFrom(src => src.Doctor))
                .ForMember(dest => dest.Patient, opt => opt.MapFrom(src => src.Patient))
                .ReverseMap();
            CreateMap<Appointment, AppointmentSummaryDTO>().ReverseMap();
            CreateMap<Appointment, CreateAppointmentDTO>().ReverseMap();
            CreateMap<Appointment, UpdateAppointmentDTO>().ReverseMap();

            // Doctor Mapping
            CreateMap<DoctorSchedule, DoctorScheduleDTO>().ReverseMap();

            // Feedback Mapping
            CreateMap<Feedback, FeedbackDetailsDTO>()
                .ForMember(dest => dest.FeedbackId, opt => opt.MapFrom(src => src.Id))  
                .ForMember(dest => dest.User, opt => opt.MapFrom(src => src.User))
                .ReverseMap();

            CreateMap<Feedback, CreateFeedbackDTO>().ReverseMap();
            CreateMap<Feedback, FeedbackResponseDTO>().ReverseMap();

            // Live Support Mapping
            CreateMap<LiveSupport, LiveSupportDTO>()
            .ForMember(dest => dest.UserName, opt => opt.Ignore()); 
            CreateMap<LiveSupportMessageDTO, string>()
                .ConvertUsing(msg => $"{msg.MessageContent}");
            CreateMap<string, LiveSupportMessageDTO>()
                .ForMember(dest => dest.MessageContent, opt => opt.MapFrom(src => src));
            CreateMap<LiveSupportMessageDTO, LiveSupport>().ReverseMap();

            // Medical Report Mapping
            CreateMap<MedicalReport, MedicalReportDetailsDTO>()
                .ForMember(dest => dest.AppointmentDetails, opt => opt.MapFrom(src => src.Appointment))
                .ReverseMap();
            CreateMap<MedicalReport, CreateMedicalReportDTO>().ReverseMap();
            CreateMap<MedicalReport, MedicalReportSummaryDTO>().ReverseMap();

            // User Mapping
            CreateMap<User, UserProfileDTO>().ReverseMap();
            CreateMap<User, RegisterUserDTO>().ReverseMap();
            CreateMap<User, UpdateUserProfileDTO>().ReverseMap();
            CreateMap<User, LoginUserDTO>().ReverseMap();

            CreateMap<UserRole, string>().ConvertUsing(role => role.ToString());
            CreateMap<string, UserRole>().ConvertUsing(role => Enum.Parse<UserRole>(role));

            CreateMap<DoctorSpecialty, string>().ConvertUsing(role => role.ToString());
            CreateMap<string, DoctorSpecialty>().ConvertUsing(role => Enum.Parse<DoctorSpecialty>(role));

            CreateMap<AppointmentStatus, string>().ConvertUsing(role => role.ToString());
            CreateMap<string, AppointmentStatus>().ConvertUsing(role => Enum.Parse<AppointmentStatus>(role));

        }
    }
}
