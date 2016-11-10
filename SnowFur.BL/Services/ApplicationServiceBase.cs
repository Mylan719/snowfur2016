using DotVVM.Framework.Controls;
using Riganti.Utils.Infrastructure.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SnowFur.BL.Services
{
    public class ApplicationServiceBase
    {
        public IUnitOfWorkProvider UnitOfWorkProvider { get; set; }

        public static void FillDataSet<T>(GridViewDataSet<T> dataSet, IQuery<T> query)
        {
            query.Skip = dataSet.PageIndex * dataSet.PageSize;
            query.Take = dataSet.PageSize;
            query.SortCriteria.Clear();
            query.AddSortCriteria(dataSet.SortExpression, dataSet.SortDescending ? SortDirection.Descending : SortDirection.Ascending);

            dataSet.TotalItemsCount = query.GetTotalRowCount();
            dataSet.Items = query.Execute();
        }
    }
}
