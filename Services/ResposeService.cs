using AutoMapper;
using Data;
using Data.Repositories;
using Services.ViewModels;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Services
{
    public interface IResponseService
    {
        bool Status { get; set; }
        List<string> Errors { get; set; }
        List<string> Success { get; set; }
        object Data { get; set; }
        void Init();
    }
    public class ResponseService:IResponseService
    {
        public bool Status { get; set; }
        public List<string> Errors { get; set; }
        public List<string> Success { get; set; }
        public object Data { get; set; }
        public ResponseService()
        {

        }
        public void Init()
        {
            Status = false;
            Errors = Success = new List<string>();
            Data = null;
        }
       
    }
}
