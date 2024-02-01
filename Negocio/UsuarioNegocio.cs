﻿using AccesoDatos;
using Dominio;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Negocio
{
    public class UsuarioNegocio
    {
        public bool Login(Usuario usuario)
        {
            AccesoBaseDatos datos = new AccesoBaseDatos();
            try
            {
                datos.SetQuery("Select Id, Email, Pass, Nombre, Apellido, UrlImagenPerfil, Admin from USERS where Email = @email and Pass = @Pass");
                datos.SetParameters("@email", usuario.Email);
                datos.SetParameters("@pass", usuario.Pass);

                datos.ExecuteReader();
                if (datos.Lector.Read())
                {
                    usuario.Id = (int)datos.Lector["Id"];
                    usuario.Email = (string)datos.Lector["Email"];
                    usuario.Pass = (string)datos.Lector["Pass"];
                    usuario.Admin = (bool)datos.Lector["Admin"];

                    if (!(datos.Lector["Nombre"] is DBNull))
                        usuario.Nombre = (string)datos.Lector["Nombre"];
                    if (!(datos.Lector["Apellido"] is DBNull))
                        usuario.Apellido = (string)datos.Lector["Apellido"];
                    if (!(datos.Lector["UrlImagenPerfil"] is DBNull))
                        usuario.UrlImagenPerfil = (string)datos.Lector["UrlImagenPerfil"];


                    return true;
                }
                return false;
            }

            catch (Exception ex)
            {

                throw ex;
            }
            finally { datos.CloseConnection(); }
        }
    }
}
