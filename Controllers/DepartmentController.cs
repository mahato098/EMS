using AutoMapper;
using EMS.Data;
using EMS.DTO.DepartmentDto;
using EMS.Model;
using EMS.Pagination;
using EMS.Uow;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace EMS.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DepartmentController : ControllerBase
    {
        private readonly ApplicationDBContext _context;
        private readonly IMapper _mapper;
        private readonly IUnitOfWork _unitOfWork;

        public DepartmentController(ApplicationDBContext context, IMapper mapper, IUnitOfWork unitOfWork)
        {
           _context = context;
            _mapper = mapper;
            _unitOfWork = unitOfWork;
        }



        [HttpPost, Route("AddDepartment")]
        public async Task<IActionResult> AddDepartment([FromBody] AddDepartmentDto department)
        {
            try
            {
                var deptExists = await _context.Departments.FirstOrDefaultAsync(x => x.ShortName == department.ShortName);

                if(deptExists == null)
                {
                    var obj = new AddDepartmentDto()
                    {
                        ShortName = department.ShortName,
                        LongName = department.LongName
                    };

                    var result = _mapper.Map<Department>(obj);
                    _context.Add(result);

                    await _unitOfWork.SaveChangesAsync();

                    return Ok(new { Message = "Department Created Successfylly!" });

                }
                else
                {
                    return BadRequest(new {Mesaage = "Department Already Exists!"});
                }

            }
            catch (Exception)
            {
                throw;
            }
        }


        [HttpGet, Route("GetAllDepartments")]
        public async Task<PagedJson<DepartmentDtos>> GetAllDepartments(int pageIndex, int pageSize, string searchName="")
        {
            try
            {
                var query = _context.Departments
                    //.Where(d => string.IsNullOrEmpty(searchName) || d.LongName.Contains(searchName))
                     //.OrderBy(x => x.ShortName)
                    .Select(d => new DepartmentDtos
                    {
                        DepartmentId = d.DepartmentId,
                        ShortName = d.ShortName,
                        LongName = d.LongName
                    });

                if (!string.IsNullOrEmpty(searchName))
                {
                    query = query.Where(d => d.LongName.Contains(searchName));
                }

                var deptList = await PagedList<DepartmentDtos>.CreateAsync(query, pageIndex, pageSize);

                var pagedJson = new PagedJson<DepartmentDtos>
                {
                    Data = deptList,
                    PageNo = deptList.PageIndex,
                    PageSize = deptList.PageSize,
                    TotalCount = deptList.TotalCount,
                    TotalPages = deptList.TotalPages
                };

                return pagedJson;
            }
            catch (Exception)
            {
                throw;
            }
        }

        [HttpGet, Route("GetDepartmentById")]
        public async Task<IActionResult> GetDepartmentById(int Id)
        {
            try
            {
                var department = await _context.Departments.FirstOrDefaultAsync(x => x.DepartmentId == Id);
                if(department != null)
                {
                    return Ok(department);
                }
                return BadRequest(new {Message="Department Name Not Found!"});
            }
            catch (Exception)
            {
                throw;
            }
        }

        [HttpPut, Route("UpdateDepartment")]
        public async Task<IActionResult> UpdateDepartment([FromBody] UpdateDepartmentDto department)
        {
            try
            {
                var obj = new UpdateDepartmentDto()
                {
                    DepartmentId = department.DepartmentId,
                    ShortName = department.ShortName,
                    LongName = department.LongName
                };

                var response = _mapper.Map<Department>(obj);

                _context.Update(response);
                await _unitOfWork.SaveChangesAsync();

                return Ok(new {Message = "Department Updated Successfully!"});

            }
            catch (Exception)
            {
                throw;
            }
        }

        [HttpDelete, Route("DeleteDepartment")]
        public async Task<IActionResult> DeleteDepartment(int Id)
        {
            try
            {
                var department = await _context.Departments.FirstOrDefaultAsync(x => x.DepartmentId == Id);
                if(department != null)
                {
                    _context.Remove(department);
                    await _unitOfWork.SaveChangesAsync();
                }
                return Ok(new { Message = "Department Deleted Successfully!" });
            }
            catch (Exception)
            {
                throw;
            }
        }

    }
}
