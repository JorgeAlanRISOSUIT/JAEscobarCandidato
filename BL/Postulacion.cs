using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BL
{
    public class Postulacion
    {
        public static (bool, string, Exception, ML.Postulacion) ProcesadoCandidato(int idCandidato)
        {
            try
            {
                using (DL.JAEscobarCandidatoEntities context = new DL.JAEscobarCandidatoEntities())
                {
                    var result = context.ProcesadoCandidato(idCandidato).SingleOrDefault();
                    if (result != null)
                    {
                        ML.Postulacion proceso = new ML.Postulacion
                        {
                            Candidato = new ML.Candidato
                            {
                                Nombre = result.NombreCandidato,
                                ApellidoPaterno = result.ApellidoPaterno,
                                ApellidoMaterno = result.ApellidoMaterno,
                                FechaNacimiento = result.FechaNacimiento,
                                Genero = result.Genero,
                                Email = result.Email,
                                Telefono = result.Telefono,
                                AutoEstima = new ML.AutoEstima(),
                                Personalidad = new ML.Personalidad(),
                                Sinceridad = new ML.Sinceridad(),
                                Estres = new ML.Estres()
                            },
                            Vacante = new ML.Vacante
                            {
                                IdVacante = result.IdVacante,
                                Nombre = result.NombreVacante,
                                FechaPublicacion = result.FechaPublicación,
                                Empresa = new ML.Empresa
                                {
                                    IdEmpresa = result.IdEmpresa,
                                    Nombre = result.NombreEmpresa
                                }
                            },
                            StatusPostulacion = new ML.StatusPostulacion
                            {
                                IdStatusPostulacion = result.IdStatusPostulacion,
                                Nombre = result.EstadoPostulacion
                            },
                            FechaPostulacion = result.FechaPostulacion
                        };
                        return (true, "", null, proceso);
                    }
                    else
                    {
                        return (false, "No existe el candidato", null, null);
                    }

                }
            }
            catch (Exception ex)
            {
                return (false, ex.Message, ex, null);
            }
        }

        public static (bool, string, Exception, ML.Postulacion) GetAllProcesados(int? value)
        {
            try
            {
                ML.Postulacion procesamiento = new ML.Postulacion { Postulaciones = new List<ML.Postulacion>() };
                using (DL.JAEscobarCandidatoEntities context = new DL.JAEscobarCandidatoEntities())
                {
                    var procesados = context.Procesados(value.GetValueOrDefault(0)).ToList();
                    if (procesados.Count > 0)
                    {
                        foreach (var item in procesados)
                        {
                            ML.Postulacion postulacion = new ML.Postulacion();
                            postulacion.Candidato = new ML.Candidato();
                            postulacion.Candidato.IdCandidato = item.IdCandidato;
                            postulacion.Candidato.Nombre = item.NombreCandidato;
                            postulacion.Candidato.ApellidoPaterno = item.ApellidoPaterno;
                            postulacion.Candidato.ApellidoMaterno = item.ApellidoMaterno;
                            postulacion.Candidato.Email = item.Email;
                            postulacion.Candidato.Genero = item.Genero;
                            postulacion.Candidato.FechaNacimiento = item.FechaNacimiento;
                            postulacion.Candidato.Telefono = item.Telefono;
                            postulacion.Vacante = new ML.Vacante();
                            postulacion.Vacante.IdVacante = item.IdVacante;
                            postulacion.Vacante.Nombre = item.NombreVacante;
                            postulacion.Vacante.FechaPublicacion = item.FechaPublicación;
                            postulacion.StatusPostulacion = new ML.StatusPostulacion();
                            postulacion.StatusPostulacion.IdStatusPostulacion = item.IdStatusPostulacion;
                            postulacion.StatusPostulacion.Nombre = item.EstadoPostulacion;
                            postulacion.Vacante.Empresa = new ML.Empresa();
                            postulacion.Vacante.Empresa.IdEmpresa = item.IdEmpresa;
                            postulacion.Vacante.Empresa.Nombre = item.NombreEmpresa;
                            procesamiento.Postulaciones.Add(postulacion);
                        }
                        return (true, "", null, procesamiento);
                    }
                    else
                    {
                        return (false, "", null, null);
                    }
                }
            }
            catch (Exception ex)
            {
                return (false, ex.Message, ex, null);
            }
        }

        public static (bool, string, Exception, ML.Postulacion) GetAll()
        {
            try
            {
                ML.Postulacion preregistros = new ML.Postulacion { Postulaciones = new List<ML.Postulacion>() };
                using (DL.JAEscobarCandidatoEntities context = new DL.JAEscobarCandidatoEntities())
                {
                    var result = context.GetAllVacante().ToList();
                    if (result.Count > 0)
                    {
                        foreach (var post in result)
                        {
                            ML.Postulacion postulacion = new ML.Postulacion
                            {
                                Candidato = new ML.Candidato
                                {
                                    IdCandidato = post.IdCandidato,
                                    Nombre = post.NombreCandidato,
                                    ApellidoPaterno = post.ApellidoPaterno,
                                    ApellidoMaterno = post.ApellidoMaterno,
                                    Email = post.Email,
                                    Genero = post.Genero,
                                    FechaNacimiento = post.FechaNacimiento,
                                    Telefono = post.Telefono,
                                    Personalidad = new ML.Personalidad
                                    {
                                        IdPersonalidad = post.IdPersonalidad.GetValueOrDefault(0),
                                        Nombre = post.NivelPersonalidad
                                    },
                                    AutoEstima = new ML.AutoEstima
                                    {
                                        IdAutoEstima = post.IdAutoEstima.GetValueOrDefault(0),
                                        Nombre = post.NivelAutoEstima
                                    },
                                    Estres = new ML.Estres
                                    {
                                        IdEstres = post.IdEstres.GetValueOrDefault(0),
                                        Nombre = post.NivelEstres
                                    },
                                    Sinceridad = new ML.Sinceridad
                                    {
                                        IdSinceridad = post.IdSinceridad.GetValueOrDefault(0),
                                        Nombre = post.NivelSinceridad
                                    }
                                },
                                StatusPostulacion = new ML.StatusPostulacion
                                {
                                    IdStatusPostulacion = post.IdStatusPostulacion,
                                    Nombre = post.EstadoPostulacion
                                },
                                Vacante = new ML.Vacante
                                {
                                    Nombre = post.NombreVacante,
                                    FechaPublicacion = post.FechaPublicación,
                                    Empresa = new ML.Empresa
                                    {
                                        IdEmpresa = post.IdEmpresa,
                                        Nombre = post.NombreEmpresa
                                    }
                                }
                            };
                            preregistros.Postulaciones.Add(postulacion);
                        }
                        return (true, "", null, preregistros);
                    }
                    else
                    {
                        return (false, "No existen preregistros", null, null);
                    }
                }
            }
            catch (Exception ex)
            {
                return (false, ex.Message, ex, null);
            }
        }

        public static (bool, string, Exception, ML.Postulacion) VacanteGetById(int idVacante)
        {
            try
            {
                ML.Postulacion preregistros = new ML.Postulacion { Postulaciones = new List<ML.Postulacion>() };
                using (DL.JAEscobarCandidatoEntities context = new DL.JAEscobarCandidatoEntities())
                {
                    var result = context.VacanteGetByIdCandidato(idVacante).ToList();
                    if (result.Count > 0)
                    {
                        foreach (var post in result)
                        {
                            ML.Postulacion postulacion = new ML.Postulacion
                            {
                                Candidato = new ML.Candidato
                                {
                                    IdCandidato = post.IdCandidato,
                                    Nombre = post.NombreCandidato,
                                    ApellidoPaterno = post.ApellidoPaterno,
                                    ApellidoMaterno = post.ApellidoMaterno,
                                    Email = post.Email,
                                    Genero = post.Genero,
                                    FechaNacimiento = new DateTime(post.FechaNacimiento.Year, post.FechaNacimiento.Month, post.FechaNacimiento.Day).Date,
                                    Telefono = post.Telefono,
                                    Personalidad = new ML.Personalidad
                                    {
                                        IdPersonalidad = post.IdPersonalidad.GetValueOrDefault(0),
                                        Nombre = post.NivelPersonalidad
                                    },
                                    AutoEstima = new ML.AutoEstima
                                    {
                                        IdAutoEstima = post.IdAutoEstima.GetValueOrDefault(0),
                                        Nombre = post.NivelAutoEstima
                                    },
                                    Estres = new ML.Estres
                                    {
                                        IdEstres = post.IdEstres.GetValueOrDefault(0),
                                        Nombre = post.NivelEstres
                                    },
                                    Sinceridad = new ML.Sinceridad
                                    {
                                        IdSinceridad = post.IdSinceridad.GetValueOrDefault(0),
                                        Nombre = post.NivelSinceridad
                                    }
                                },
                                StatusPostulacion = new ML.StatusPostulacion
                                {
                                    IdStatusPostulacion = post.IdStatusPostulacion,
                                    Nombre = post.EstadoPostulacion
                                },
                                Vacante = new ML.Vacante
                                {
                                    Nombre = post.NombreVacante,
                                    FechaPublicacion = new DateTime(post.FechaPublicación.Year, post.FechaPublicación.Month, post.FechaPublicación.Day).Date,
                                    Empresa = new ML.Empresa
                                    {
                                        IdEmpresa = post.IdEmpresa,
                                        Nombre = post.NombreEmpresa
                                    }
                                }
                            };
                            preregistros.Postulaciones.Add(postulacion);
                        }
                        return (true, "", null, preregistros);
                    }
                    else
                    {
                        return (false, "No hay conincidencia en la búsqueda", null, null);
                    }
                }
            }
            catch (Exception ex)
            {
                return (false, ex.Message, ex, null);
            }
        }

        public static (bool, string, Exception, ML.Postulacion) VacanteGetById(int idVacante, int idStatusPostulacion)
        {
            try
            {
                ML.Postulacion preregistros = new ML.Postulacion { Postulaciones = new List<ML.Postulacion>() };
                using (DL.JAEscobarCandidatoEntities context = new DL.JAEscobarCandidatoEntities())
                {
                    var result = context.VacanteStatusGetByIdCandidato(idVacante, idStatusPostulacion).ToList();
                    if (result.Count > 0)
                    {
                        foreach (var post in result)
                        {
                            ML.Postulacion postulacion = new ML.Postulacion
                            {
                                Candidato = new ML.Candidato
                                {
                                    IdCandidato = post.IdCandidato,
                                    Nombre = post.NombreCandidato,
                                    ApellidoPaterno = post.ApellidoPaterno,
                                    ApellidoMaterno = post.ApellidoMaterno,
                                    Email = post.Email,
                                    Genero = post.Genero,
                                    FechaNacimiento = post.FechaNacimiento,
                                    Telefono = post.Telefono,
                                    Personalidad = new ML.Personalidad
                                    {
                                        IdPersonalidad = post.IdPersonalidad.GetValueOrDefault(0),
                                        Nombre = post.NivelPersonalidad
                                    },
                                    AutoEstima = new ML.AutoEstima
                                    {
                                        IdAutoEstima = post.IdAutoEstima.GetValueOrDefault(0),
                                        Nombre = post.NivelAutoEstima
                                    },
                                    Estres = new ML.Estres
                                    {
                                        IdEstres = post.IdEstres.GetValueOrDefault(0),
                                        Nombre = post.NivelEstres
                                    },
                                    Sinceridad = new ML.Sinceridad
                                    {
                                        IdSinceridad = post.IdSinceridad.GetValueOrDefault(0),
                                        Nombre = post.NivelSinceridad
                                    }
                                },
                                StatusPostulacion = new ML.StatusPostulacion
                                {
                                    IdStatusPostulacion = post.IdStatusPostulacion,
                                    Nombre = post.EstadoPostulacion
                                },
                                Vacante = new ML.Vacante
                                {
                                    Nombre = post.NombreVacante,
                                    FechaPublicacion = post.FechaPublicación,
                                    Empresa = new ML.Empresa
                                    {
                                        IdEmpresa = post.IdEmpresa,
                                        Nombre = post.NombreEmpresa
                                    }
                                }
                            };
                            preregistros.Postulaciones.Add(postulacion);
                        }
                        return (true, "", null, preregistros);
                    }
                    else
                    {
                        return (false, "No hay conincidencia en la búsqueda", null, null);
                    }
                }
            }
            catch (Exception ex)
            {
                return (false, ex.Message, ex, null);
            }
        }

        public static (bool, string, Exception, ML.Postulacion) VacanteGetByIdPostulacion(int idStatusPostulacion)
        {
            try
            {
                ML.Postulacion preregistros = new ML.Postulacion { Postulaciones = new List<ML.Postulacion>() };
                using (DL.JAEscobarCandidatoEntities context = new DL.JAEscobarCandidatoEntities())
                {
                    var result = context.StatusPostulacionGetById(idStatusPostulacion).ToList();
                    if (result.Count > 0)
                    {
                        foreach (var post in result)
                        {
                            ML.Postulacion postulacion = new ML.Postulacion
                            {
                                Candidato = new ML.Candidato
                                {
                                    IdCandidato = post.IdCandidato,
                                    Nombre = post.NombreCandidato,
                                    ApellidoPaterno = post.ApellidoPaterno,
                                    ApellidoMaterno = post.ApellidoMaterno,
                                    Email = post.Email,
                                    Genero = post.Genero,
                                    FechaNacimiento = post.FechaNacimiento,
                                    Telefono = post.Telefono,
                                    Personalidad = new ML.Personalidad
                                    {
                                        IdPersonalidad = post.IdPersonalidad.GetValueOrDefault(0),
                                        Nombre = post.NivelPersonalidad
                                    },
                                    AutoEstima = new ML.AutoEstima
                                    {
                                        IdAutoEstima = post.IdAutoEstima.GetValueOrDefault(0),
                                        Nombre = post.NivelAutoEstima
                                    },
                                    Estres = new ML.Estres
                                    {
                                        IdEstres = post.IdEstres.GetValueOrDefault(0),
                                        Nombre = post.NivelEstres
                                    },
                                    Sinceridad = new ML.Sinceridad
                                    {
                                        IdSinceridad = post.IdSinceridad.GetValueOrDefault(0),
                                        Nombre = post.NivelSinceridad
                                    }
                                },
                                StatusPostulacion = new ML.StatusPostulacion
                                {
                                    IdStatusPostulacion = post.IdStatusPostulacion,
                                    Nombre = post.EstadoPostulacion
                                },
                                Vacante = new ML.Vacante
                                {
                                    Nombre = post.NombreVacante,
                                    FechaPublicacion = post.FechaPublicación,
                                    Empresa = new ML.Empresa
                                    {
                                        IdEmpresa = post.IdEmpresa,
                                        Nombre = post.NombreEmpresa
                                    }
                                }
                            };
                            preregistros.Postulaciones.Add(postulacion);
                        }
                        return (true, "", null, preregistros);
                    }
                    else
                    {
                        return (false, "No hay conincidencia en la búsqueda", null, null);
                    }
                }
            }
            catch (Exception ex)
            {
                return (false, ex.Message, ex, null);
            }
        }

    }
}
