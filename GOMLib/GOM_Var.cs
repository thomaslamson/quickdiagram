using System;
using System.Collections;
using System.Xml;
using System.Drawing;


namespace GOMLib
{
    public class Variablelist
    {
        public ArrayList varlist = new ArrayList();  //list store XMLline
        public string access(string type, string name)
        {
            for (int i = 0; i < varlist.Count; i++)
            {
                XMLline templine = ((XMLline)varlist[i]);
                if (string.Compare(templine.type, type, true) == 0)
                {
                    return templine.access(name);
                }
            }
            return "";
        }
        public bool modify(string type, string name, string mv)
        {
            for (int i = 0; i < varlist.Count; i++)
            {
                XMLline templine = ((XMLline)varlist[i]);
                if (string.Compare(templine.type, type, true) == 0)
                {
                    return templine.modify(name, mv);
                    return true;
                }
            }
            return false ;
        }
        public Variablelist clone()
        {
            Variablelist tempvar = new Variablelist();
            if (varlist != null)
            {
                for (int i = 0; i < varlist.Count; i++)
                {
                    tempvar.varlist.Add(((XMLline)varlist[i]).clone());
                }
            }
            return tempvar;
        }

    }

    public class XMLline
    {
        public string type = "-";
        public ArrayList attributelist = new ArrayList();    //list store XMLAttribute
        public XMLline clone()
        {
            XMLline templine = new XMLline();
            if (type != null)
                templine.type = type;
            if (attributelist != null)
            {
                for (int i=0; i < attributelist.Count; i ++)
                {
                   templine.attributelist.Add(((XMLatt)attributelist[i]).clone());
                }
            }
            return templine;
        }

        public string access(string name)
        {
            for (int j = 0; j < attributelist.Count; j++)
            {
                XMLatt tempatt = ((XMLatt)attributelist[j]);
                if (string.Compare(tempatt.aname, name, true) == 0)
                    return tempatt.acontent;
            }
            return "";
        }

        public bool modify(string name, string mv)
        {
            for (int j = 0; j < attributelist.Count; j++)
            {
                XMLatt tempatt = ((XMLatt)attributelist[j]);
                if (string.Compare(tempatt.aname, name, true) == 0)
                {
                    tempatt.acontent = mv;
                    return true;
                }
            }
            return false;
        }
    }

    public class XMLatt
    {
        public string aname;
        public string acontent;
        public XMLatt clone()
        {
            XMLatt temp = new XMLatt();
            temp.aname = aname;
            temp.acontent = acontent;
            return temp;
        }
    }
}
