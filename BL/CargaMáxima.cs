using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Data.OleDb;
using System.Data;
using System.Globalization;

namespace BL
{
    public class CargaMáxima
    {

        public static (bool, string, Exception) ComprobarExtension(string archivo)
        {
            string direccionAbsoluta = @"~\..\..\..\..\Files";
            string extension = Path.GetExtension(direccionAbsoluta + archivo);
            if (extension == ".xlsx")
            {
                return (true, Path.GetFullPath(direccionAbsoluta), null);
            }
            else
            {
                return (false, "Solo se aceptan archivos de excel", null);
            }
        }

        public static (bool, string, Exception, ML.Candidato) AddExcel(string conexion)
        {
            try
            {
                ML.Candidato registros = new ML.Candidato { Candidatos = new List<ML.Candidato>() };
                using (OleDbConnection context = new OleDbConnection(conexion))
                {
                    OleDbCommand command = context.CreateCommand();
                    command.CommandText = "SELECT * FROM [Sheet1$]";
                    command.Connection = context;
                    context.Open();
                    OleDbDataAdapter adapter = new OleDbDataAdapter(command);
                    DataTable table = new DataTable();
                    adapter.Fill(table);
                    if(table.Rows.Count > 0)
                    {
                        foreach(DataRow excelRow in table.Rows)
                        {
                            ML.Candidato candidato = new ML.Candidato();
                            candidato.Nombre = excelRow[0].ToString();
                            candidato.ApellidoPaterno = excelRow[1].ToString();
                            candidato.ApellidoMaterno = excelRow[2].ToString();
                            candidato.Email = excelRow[3].ToString();
                            candidato.Genero = excelRow[4].ToString();
                            candidato.FechaNacimiento = DateTime.Parse(excelRow[5].ToString());
                            candidato.Telefono = excelRow[6].ToString();
                            candidato.Sinceridad = new ML.Sinceridad();
                            candidato.Sinceridad.IdSinceridad = !string.IsNullOrEmpty(excelRow[7].ToString()) ? int.Parse(excelRow[7].ToString()): 0;
                            candidato.AutoEstima = new ML.AutoEstima();
                            candidato.AutoEstima.IdAutoEstima = !string.IsNullOrEmpty(excelRow[8].ToString()) ? int.Parse(excelRow[8].ToString()) : 0;
                            candidato.Personalidad = new ML.Personalidad();
                            candidato.Personalidad.IdPersonalidad = !string.IsNullOrEmpty(excelRow[9].ToString()) ? int.Parse(excelRow[9].ToString()) : 0;
                            candidato.Estres = new ML.Estres();
                            candidato.Estres.IdEstres = !string.IsNullOrEmpty(excelRow[10].ToString()) ? int.Parse(excelRow[10].ToString()) : 0;
                            registros.Candidatos.Add(candidato);
                        }
                        return (true, "", null, registros);
                    }
                    else
                    {
                        return (false, "No existen registros", null, null);
                    }
                }
            }
            catch (Exception ex)
            {
                return (false, ex.Message, ex, null);
            }
        }
        public static (bool, string, Exception, ML.ExcelErrors) ValidarCampos(List<ML.Candidato> candidatos)
        {
            try
            {
                ML.ExcelErrors respuestas = new ML.ExcelErrors { Errores = new List<ML.ExcelErrors>() };
                foreach (var item in candidatos)
                {
                    ML.ExcelErrors obj = new ML.ExcelErrors();
                    obj.Mensaje += string.IsNullOrEmpty(item.Nombre) ? "Falta el nombre" : "";
                    obj.Mensaje += string.IsNullOrEmpty(item.ApellidoPaterno) ? "Falta el apellido paterno" : "";
                    obj.Mensaje += string.IsNullOrEmpty(item.ApellidoMaterno) ? "Falta el apellido materno" : "";
                    obj.Mensaje += item.FechaNacimiento == null ? "no existe la fecha" : "";
                    obj.Mensaje += string.IsNullOrEmpty(item.Email) ? "falta el correo" : "";
                    obj.Mensaje += string.IsNullOrEmpty(item.Genero) ? "falta el genero" : "";
                    obj.Mensaje += string.IsNullOrEmpty(item.Telefono) ? "falta el numero de contacto" : "";
                    if (!string.IsNullOrEmpty(obj.Mensaje))
                    {
                        respuestas.Errores.Add(obj);
                    }
                }
                if (respuestas.Errores.Count > 0)
                {
                    return (true, "Errores encontrados", null, respuestas);
                }
                else
                {
                    return (false, "", null, null);
                }
            }
            catch (Exception ex)
            {
                return (false, ex.Message, ex, null);
            }
        }
    }
}
