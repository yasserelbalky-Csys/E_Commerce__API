using BLL.DTOs.CurrentProductBalanceDtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.Contracts
{
    public interface ICurrentProductBalanceService
    {
        public bool Insert(CurrentProductBalanceInsertDto init);

        public bool update(CurrentProductBalanceUpdateDto init);

        public IEnumerable<CurrentProductBalanceListDto> GetAll();

        public CurrentProductBalanceListDto GetById(int id);

        public bool DeleteById(int id);
    }
}