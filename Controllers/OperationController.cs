﻿
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MyFinances.WebApi.Models;
using MyFinances.WebApi.Models.Converters;
using MyFinances.WebApi.Models.Domains;
using MyFinances.WebApi.Models.Dtos;
using MyFinances.WebApi.Models.Response;

namespace MyFinances.WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OperationController : ControllerBase
    {
        private readonly UnitOfWork _unitOfWork;

        public OperationController(UnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        [HttpGet]
        public DataResponse<IEnumerable<OperationDto>> Get()
        {
            var response = new DataResponse<IEnumerable<OperationDto>>();

            try
            {
                response.Data = _unitOfWork.Operation.Get().ToDtos();
            }
            catch (Exception exception)
            {
                // logowanie do pliku
                response.Errors.Add(new Error(exception.Source, exception.Message));
            }


            return response;
        }

        [HttpGet("{numberOfRecords}, {numberOfPage}")]
        public DataResponse<IEnumerable<OperationDto>> GetPaggined(int numberOfRecords, int numberOfPage)
        {
            var response = new DataResponse<IEnumerable<OperationDto>>();

            try
            {
                response.Data = _unitOfWork.Operation.Get(numberOfRecords, numberOfPage).ToDtos();
            }
            catch (Exception exception)
            {
                // logowanie do pliku
                response.Errors.Add(new Error(exception.Source, exception.Message));
            }


            return response;
        }

        /// <summary>
        /// Get operation by id.
        /// </summary>
        /// <param name="id">Operation Id</param>
        /// <returns>DataResponse - Operation Dto</returns>
        [HttpGet("{id}")]
        public DataResponse<OperationDto> Get(int id)
        {
            var response = new DataResponse<OperationDto>();

            try
            {
                response.Data = _unitOfWork.Operation.Get(id)?.ToDto();
            }
            catch (Exception exception)
            {
                // logowanie do pliku
                response.Errors.Add(new Error(exception.Source, exception.Message));
            }


            return response;
        }

        [HttpPost]
        public DataResponse<int> Add(OperationDto operationDto)
        {
            var response = new DataResponse<int>();

            try
            {
                var operation = operationDto.ToDao();
                _unitOfWork.Operation.Add(operation);
                _unitOfWork.Complete();
                response.Data = operation.Id;
            }
            catch (Exception exception)
            {
                // logowanie do pliku
                response.Errors.Add(new Error(exception.Source, exception.Message));
            }

            return response;
        }

        [HttpPut]
        public Response Update(OperationDto operation)
        {
            var response = new Response();

            try
            {
                _unitOfWork.Operation.Update(operation.ToDao());
                _unitOfWork.Complete();
            }
            catch (Exception exception)
            {
                // logowanie do pliku
                response.Errors.Add(new Error(exception.Source, exception.Message));
            }

            return response;
        }

        [HttpDelete("{id}")]
        public Response Delete(int id)
        {
            var response = new Response();

            try
            {
                _unitOfWork.Operation.Delete(id);
                _unitOfWork.Complete();
            }
            catch (Exception exception)
            {
                // logowanie do pliku
                response.Errors.Add(new Error(exception.Source, exception.Message));
            }

            return response;
        }
    }
}
