using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using core.aplicacao;
using dominio;
namespace core.Utils
{
    public class ResultadoToDataTable
    {
        public static DataTable cat_to_datatable(Resultado input)
        {
            DataTable data=new DataTable();
            data.Columns.Add(new DataColumn("cod", typeof(int)));
            data.Columns.Add(new DataColumn("nome_cat", typeof(string)));

            int a = input.Entidades.Count;
            for (int i = 0;i< a;i++)
            {

                Categoria cat = (Categoria)input.Entidades.ElementAt(i);
                DataRow dr = data.NewRow();
                dr[0] = cat.Id;
                dr[1] = cat.Nome;
                data.Rows.Add(dr);
            }
            return data;
        }
    
        public static DataTable g_pre_to_datatable(Resultado input)
        {
            DataTable data = new DataTable();
            data.Columns.Add(new DataColumn("Id", typeof(int)));
            data.Columns.Add(new DataColumn("Nome", typeof(string)));

            int a = input.Entidades.Count;
            for (int i = 0; i < a; i++)
            {

                Grupo_Precificacao cat = (Grupo_Precificacao)input.Entidades.ElementAt(i);
                DataRow dr = data.NewRow();
                dr[0] = cat.Id;
                dr[1] = cat.Nome;
                data.Rows.Add(dr);
            }
            return data;
        }
    }
}
