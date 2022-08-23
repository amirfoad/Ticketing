using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ticketing.Services.Profiles
{
    public interface ICreateMapper<TSource>
    {
        void Map(Profile profile)
        {
            profile.CreateMap(typeof(TSource), GetType()).ReverseMap();
        }
    }
}
