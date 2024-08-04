using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace API.ControleTarefas.Domain.Services
{
    public static class GeneralFunction
    {
        public static int PageTotal<T>(this List<T> inputList, int pageSize)
        {
            if (inputList.Count == 0)
            {
                return 0;
            }

            pageSize = PageSizeAdjust(pageSize);
            decimal num = Convert.ToDecimal(inputList.Count) / Convert.ToDecimal(pageSize);
            decimal num2 = num - Math.Truncate(num);
            num = Math.Truncate(num);
            if (num2 > 0m)
            {
                num += 1m;
            }

            return decimal.ToInt32(num);
        }

        public static bool PageHasNext<T>(this List<T> inputList, int pageSize, int page)
        {
            page = PageAdjust(page);
            pageSize = PageSizeAdjust(pageSize);
            if (pageSize * page + pageSize >= inputList.Count)
            {
                return false;
            }

            return true;
        }

        public static List<T> Page<T>(this List<T> inputList, int pageSize, int page)
        {
            if (pageSize > 0)
            {
                page = PageAdjust(page);
                pageSize = PageSizeAdjust(pageSize);
                try
                {
                    int num = inputList?.Count ?? 0;
                    if (pageSize * page + pageSize > num)
                    {
                        return inputList?.GetRange(pageSize * page, num - pageSize * page);
                    }

                    return inputList?.GetRange(pageSize * page, pageSize);
                }
                catch
                {
                    inputList.Clear();
                    return inputList;
                }
            }

            return inputList;
        }

        private static int PageAdjust(int page)
        {
            page = ((page > 0) ? (page - 1) : 0);
            return page;
        }

        private static int PageSizeAdjust(int pageSize)
        {
            if (pageSize <= 0)
            {
                pageSize = 100;
            }

            return pageSize;
        }
    }
}
