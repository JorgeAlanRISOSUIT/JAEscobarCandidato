using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.OleDb;
using System.Globalization;

namespace BL
{
    public class Candidato
    {
        public static (bool, string, Exception) AddRange(IEnumerable<ML.Candidato> candidatos)
        {
            try
            {
                using (DL.JAEscobarCandidatoEntities context = new DL.JAEscobarCandidatoEntities())
                {
                    using (var transaccion = context.Database.BeginTransaction())
                    {
                        List<DL.Candidato> candidatoes = new List<DL.Candidato>();
                        try
                        {
                            foreach (var item in candidatos)
                            {
                                DL.Candidato candidato = new DL.Candidato();
                                candidato.Nombre = item.Nombre;
                                candidato.ApellidoPaterno = item.ApellidoPaterno;
                                candidato.ApellidoMaterno = item.ApellidoMaterno;
                                candidato.Email = item.Email;
                                candidato.FechaNacimiento = item.FechaNacimiento;
                                candidato.Genero = item.Genero;
                                candidato.Telefono = item.Telefono;
                                candidato.IdAutoEstima = item.AutoEstima.IdAutoEstima != 0 ? item.AutoEstima.IdAutoEstima : (int?)null;
                                candidato.IdSinceridad = item.Sinceridad.IdSinceridad != 0 ? item.Sinceridad.IdSinceridad : (int?)null;
                                candidato.IdPersonalidad = item.Personalidad.IdPersonalidad != 0 ? item.Personalidad.IdPersonalidad : (int?)null;
                                candidato.IdSinceridad = item.Sinceridad.IdSinceridad != 0 ? item.Sinceridad.IdSinceridad : (int?)null;
                                candidatoes.Add(candidato);
                            }
                            context.Candidatoes.AddRange(candidatoes);
                            int row = context.SaveChanges();
                            if (row > 0)
                            {
                                transaccion.Commit();
                                return (true, "", null);
                            }
                            else
                            {
                                transaccion.Rollback();
                                return (false, "No se registro", null);
                            }
                        }
                        catch (Exception ex)
                        {
                            transaccion.Rollback();
                            return (false, ex.Message, null);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                return (false, ex.Message, ex);
            }
        }
        public static (bool, string, Exception, ML.Candidato) GetAll()
        {
            try
            {
                ML.Candidato lista = new ML.Candidato { Candidatos = new List<ML.Candidato>() };
                using (DL.JAEscobarCandidatoEntities context = new DL.JAEscobarCandidatoEntities())
                {
                    var result = context.GetAllCandidato().ToList();
                    if (result.Count > 0)
                    {
                        foreach (var resultObj in result)
                        {
                            ML.Candidato candidato = new ML.Candidato()
                            {
                                IdCandidato = resultObj.IdCandidato,
                                Nombre = resultObj.NombreCandidato,
                                ApellidoPaterno = resultObj.ApellidoPaterno,
                                ApellidoMaterno = resultObj.ApellidoMaterno,
                                Email = resultObj.Email,
                                Genero = resultObj.Genero,
                                FechaNacimiento = resultObj.FechaNacimiento,
                                Telefono = resultObj.Telefono,
                                Personalidad = new ML.Personalidad
                                {
                                    Nombre = resultObj.NivelPersonalidad
                                },
                                AutoEstima = new ML.AutoEstima
                                {
                                    Nombre = resultObj.NivelAutoEstima
                                },
                                Estres = new ML.Estres
                                {
                                    Nombre = resultObj.NivelEstres
                                },
                                Sinceridad = new ML.Sinceridad
                                {
                                    Nombre = resultObj.NivelSincero
                                }
                            };
                            lista.Candidatos.Add(candidato);
                        }
                        return (true, "", null, lista);
                    }
                    else
                    {
                        return (false, "No se encuentran candidatos", null, null);
                    }
                }
            }
            catch (Exception ex)
            {
                return (false, ex.Message, ex, null);
            }
        }

        public static (bool, string, Exception, ML.Candidato) GetById(int idCandidato)
        {
            try
            {
                using (DL.JAEscobarCandidatoEntities context = new DL.JAEscobarCandidatoEntities())
                {
                    var result = context.GetByIdCandidato(idCandidato).SingleOrDefault();
                    if (result != null)
                    {
                        ML.Candidato candidato = new ML.Candidato
                        {
                            IdCandidato = result.IdCandidato,
                            Nombre = result.NombreCandidato,
                            ApellidoPaterno = result.ApellidoPaterno,
                            ApellidoMaterno = result.ApellidoMaterno,
                            Email = result.Email,
                            Genero = result.Genero,
                            FechaNacimiento = result.FechaNacimiento,
                            Telefono = result.Telefono,
                            Personalidad = new ML.Personalidad
                            {
                                IdPersonalidad = result.IdPersonalidad.HasValue ? result.IdPersonalidad.Value : 0,
                                Nombre = result.NivelPersonalidad
                            },
                            AutoEstima = new ML.AutoEstima
                            {
                                IdAutoEstima = result.IdAutoEstima.HasValue ? result.IdAutoEstima.Value : 0,
                                Nombre = result.NivelAutoEstima
                            },
                            Estres = new ML.Estres
                            {
                                IdEstres = result.IdEstres.HasValue ? result.IdEstres.Value : 0,
                                Nombre = result.NivelEstres
                            },
                            Sinceridad = new ML.Sinceridad
                            {
                                IdSinceridad = result.IdSinceridad.HasValue ? result.IdSinceridad.Value : 0,
                                Nombre = result.NivelSincero
                            }
                        };
                        return (true, "", null, candidato);
                    }
                    else
                    {
                        return (false, "No se encuentra el candidato", null, null);
                    }
                }
            }
            catch (Exception ex)
            {
                return (false, ex.Message, ex, null);
            }
        }

        public static (bool, string, Exception) Add(ML.Candidato candidato)
        {
            try
            {
                using (DL.JAEscobarCandidatoEntities context = new DL.JAEscobarCandidatoEntities())
                {
                    int row = context.AddCandidato(candidato.Nombre, candidato.ApellidoPaterno, candidato.ApellidoMaterno, candidato.Email, candidato.Genero, candidato.FechaNacimiento, candidato.Telefono, candidato.Sinceridad.IdSinceridad, candidato.AutoEstima.IdAutoEstima, candidato.Personalidad.IdPersonalidad, candidato.Estres.IdEstres);
                    if (row > 0)
                    {
                        return (true, "", null);
                    }
                    else
                    {
                        return (false, "No se agrego el candidato", null);
                    }
                }
            }
            catch (Exception ex)
            {
                return (false, ex.Message, ex);
            }
        }

        public static (bool, string, Exception) UpdateCandidato(ML.Candidato candidato)
        {
            try
            {
                using (DL.JAEscobarCandidatoEntities context = new DL.JAEscobarCandidatoEntities())
                {
                    int row = context.UpdateCandidato(candidato.IdCandidato, candidato.Nombre, candidato.ApellidoPaterno, candidato.ApellidoMaterno, candidato.Email, candidato.Genero, candidato.FechaNacimiento, candidato.Telefono, candidato.Sinceridad.IdSinceridad, candidato.AutoEstima.IdAutoEstima, candidato.Personalidad.IdPersonalidad, candidato.Estres.IdEstres);
                    if (row > 0)
                    {
                        return (true, "", null);
                    }
                    else
                    {
                        return (false, "No se encuentra el candidato", null);
                    }
                }
            }
            catch (Exception ex)
            {
                return (false, ex.Message, ex);
            }
        }

        public static (bool, string, Exception) DeleteCandidato(int idCandidato)
        {
            try
            {
                using (DL.JAEscobarCandidatoEntities context = new DL.JAEscobarCandidatoEntities())
                {
                    int row = context.DeleteCandidato(idCandidato);
                    if (row > 0)
                    {
                        return (true, "", null);
                    }
                    else
                    {
                        return (false, "No se encuentre el candidato", null);
                    }
                }
            }
            catch (Exception ex)
            {
                return (false, ex.Message, ex);
            }
        }
    }
}
