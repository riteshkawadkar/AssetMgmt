using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WMS.Data;
using WMS.Data.Request;
using WMS.Data.Worfklow;
using WMS.Data.Workflow;
using WMS.Models;
using WMS.Models.Worflow;

namespace WMS.Mappings
{
    public class Maps : Profile
    {
        public Maps()
        {

            CreateMap<Employee, EmployeeVM>().ReverseMap();


            CreateMap<Company, CompanyVM>().ReverseMap();
            CreateMap<Facility, FacilityVM>().ReverseMap();
            CreateMap<Location, LocationVM>().ReverseMap();
            CreateMap<Department, DepartmentVM>().ReverseMap();
            CreateMap<SubDepartment, SubDepartmentVM>().ReverseMap();
            CreateMap<DArea, AreaVM>().ReverseMap();
            CreateMap<Room, RoomVM>().ReverseMap();
            CreateMap<Line, LineVM>().ReverseMap();
            CreateMap<EquipmentType, EquipmentTypeVM>().ReverseMap();
            CreateMap<Equipment, EquipmentVM>().ReverseMap();
            CreateMap<Instrument, InstrumentVM>().ReverseMap();
            CreateMap<ITAsset, ITAssestVM>().ReverseMap();

            CreateMap<RequestType, RequestTypeVM>().ReverseMap();
            CreateMap<RequestType, RequestTypeEditVM>().ReverseMap();
            CreateMap<MyRequest, MyRequestVM>().ReverseMap();


            CreateMap<RequestWorkflow, RequestWorkflowVM>().ReverseMap();
            CreateMap<WorkflowPasswordChange, WorkflowPasswordChangeVM>().ReverseMap();
            CreateMap<WorkflowGrantAccess, WorkflowGrantAccessVM>().ReverseMap();




        }
    }
}
