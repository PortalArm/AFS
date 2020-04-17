using ActualFileStorage.BLL.DTOs;
using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ActualFileStorage.BLL.Services.Interfaces
{
    public interface IShortenerService
    {
        IMapper Mapper { get; }

        ViewIdDTO GetIdsByLink(int? callerId, string link);
    }
}
