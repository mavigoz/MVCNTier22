using Project.DAL.Context;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project.BLL.DesingPattern.SingletonPatterns
{
    public class DBTool
    {
        DBTool() { }
        static MyContext _dbIstance;
        public static MyContext DbIstance { get { 
            if (_dbIstance == null)
                {  _dbIstance = new MyContext(); }
            
            return _dbIstance;
            } }
    }
}
