using AccesoDatos;
using Dominio;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Negocio
{
    public class ArticuloNegocio
    {
        public List<Articulo> Listar(string id = "", string consulta = "")
        {
            List<Articulo> listaArticulos = new List<Articulo>();
            AccesoBaseDatos datos = new AccesoBaseDatos();

            try
            {
                if (consulta == "")
                    consulta = "Select A.Id, Codigo, Nombre, A.Descripcion, IdMarca, IdCategoria, ImagenUrl, Precio,\r\nC.Descripcion as Categoria,\r\nM.Descripcion as Marca\r\nfrom ARTICULOS A, CATEGORIAS C, MARCAS M\r\nwhere IdMarca = M.Id and IdCategoria = C.Id";
                if (id != "")
                    consulta += $" and A.Id = {id}";

                datos.SetQuery(consulta);
                datos.ExecuteReader();

                while (datos.Lector.Read())
                {
                    Articulo articulo = new Articulo();
                    articulo.Id = (int)datos.Lector["Id"];
                    articulo.Codigo = (string)datos.Lector["Codigo"];
                    articulo.Nombre = (string)datos.Lector["Nombre"];
                    articulo.Descripcion = (string)datos.Lector["Descripcion"];
                    articulo.Precio = Math.Round((decimal)datos.Lector["Precio"], 3);

                    articulo.Marca = new Marca();
                    articulo.Marca.Id = (int)datos.Lector["IdMarca"];
                    articulo.Marca.Descripcion = (string)datos.Lector["Marca"];

                    articulo.Categoria = new Categoria();
                    articulo.Categoria.Id = (int)datos.Lector["IdCategoria"];
                    articulo.Categoria.Descripcion = (string)datos.Lector["Categoria"];

                    if (!(datos.Lector["ImagenUrl"] is DBNull))
                        articulo.ImagenUrl = (string)datos.Lector["ImagenUrl"];

                    listaArticulos.Add(articulo);
                }

                return listaArticulos;
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally { datos.CloseConnection(); }
        }
        public void AgregarNuevo(Articulo nuevo)
        {
            AccesoBaseDatos datos = new AccesoBaseDatos();

            try
            {
                datos.SetQuery("Insert into ARTICULOS (Codigo, Nombre, Descripcion, IdMarca, IdCategoria, ImagenUrl, Precio) values (@Codigo, @Nombre, @Descripcion, @IdMarca, @IdCategoria, @ImagenUrl, @Precio)");
                datos.SetParameters("@Codigo", nuevo.Codigo);
                datos.SetParameters("@Nombre", nuevo.Nombre);
                datos.SetParameters("@Descripcion", nuevo.Descripcion);
                datos.SetParameters("@IdMarca", nuevo.Marca.Id);
                datos.SetParameters("@IdCategoria", nuevo.Categoria.Id);
                datos.SetParameters("@ImagenUrl", nuevo.ImagenUrl);
                datos.SetParameters("@Precio", nuevo.Precio);

                datos.ExecuteAction();
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally { datos.CloseConnection(); }
        }


        public void ModificarArticulo(Articulo articulo)
        {
            AccesoBaseDatos datos = new AccesoBaseDatos();

            try
            {
                datos.SetQuery("Update ARTICULOS set Codigo = @Codigo, Nombre = @Nombre, Descripcion = @Descripcion, IdMarca = @IdMarca, IdCategoria = @IdCategoria, ImagenUrl = @ImagenUrl, Precio = @Precio where Id = @Id");
                datos.SetParameters("@Codigo", articulo.Codigo);
                datos.SetParameters("@Nombre", articulo.Nombre);
                datos.SetParameters("@Descripcion", articulo.Descripcion);
                datos.SetParameters("@IdMarca", articulo.Marca.Id);
                datos.SetParameters("@IdCategoria", articulo.Categoria.Id);
                datos.SetParameters("@ImagenUrl", articulo.ImagenUrl);
                datos.SetParameters("@Precio", articulo.Precio);
                datos.SetParameters("@Id", articulo.Id);

                datos.ExecuteAction();
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally { datos.CloseConnection(); }
        }

        public void EliminarArticulo(Articulo articulo)
        {
            AccesoBaseDatos datos = new AccesoBaseDatos();

            try
            {
                datos.SetQuery("Delete from ARTICULOS where Id = @Id");
                datos.SetParameters("@Id", articulo.Id);

                datos.ExecuteAction();
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        public List<Articulo> Filtrar(string campo, string tipo, string filtro = "", string filtroSecundario = "")
        {
            List<Articulo> listaFiltrada = new List<Articulo>();
            AccesoBaseDatos datos = new AccesoBaseDatos();

            try
            {
                string consulta = "Select A.Id, Codigo, Nombre, A.Descripcion, IdMarca, IdCategoria, ImagenUrl, Precio,\r\nC.Descripcion as Categoria,\r\nM.Descripcion as Marca\r\nfrom ARTICULOS A, CATEGORIAS C, MARCAS M\r\nwhere IdMarca = M.Id and IdCategoria = C.Id and ";
                if (campo == "Artículo")
                {
                    switch (tipo)
                    {
                        case "Nombre exacto":
                            consulta += $"Nombre = '{filtro}'";
                            break;
                        default:
                            consulta += $"Nombre like '%{filtro}%'";
                            break;
                    }

                }
                else if (campo == "Categoría")
                {
                    switch (tipo)
                    {
                        case "Celulares":
                            consulta += "C.Descripcion = 'Celulares'";
                            break;
                        case "Televisores":
                            consulta += "C.Descripcion = 'Televisores'";
                            break;
                        case "Media":
                            consulta += "C.Descripcion = 'Media'";
                            break;
                        case "Audio":
                            consulta += "C.Descripcion = 'Audio'";
                            break;
                        default:
                            consulta += $"C.Descripcion like '%{filtro}%'";
                            break;
                    }
                }
                else if (campo == "Marca")
                {
                    switch (tipo)
                    {
                        case "Samsung":
                            consulta += "M.Descripcion = 'Samsung'";
                            break;
                        case "Apple":
                            consulta += "M.Descripcion = 'Apple'";
                            break;
                        case "Sony":
                            consulta += "M.Descripcion = 'Sony'";
                            break;
                        case "Huawei":
                            consulta += "M.Descripcion = 'Huawei'";
                            break;
                        case "Motorola":
                            consulta += "M.Descripcion = 'Motorola'";
                            break;
                        default:
                            consulta += $"M.Descripcion like '%{filtro}%'";
                            break;
                    }
                }
                else
                {
                    switch (tipo)
                    {
                        case "Hasta":
                            consulta += $"Precio <= {filtro}";
                            break;
                        case "Más de":
                            consulta += $"Precio > {filtro}";
                            break;
                        case "Entre":
                            consulta += $"Precio >= {filtro} and Precio <= {filtroSecundario}";
                            break;
                        default:
                            consulta += $"Precio = {filtro}";
                            break;
                    }
                }
                listaFiltrada = Listar(consulta: consulta);

                return listaFiltrada;
            }
            catch (Exception ex)
            {

                throw ex;
            }
            finally { datos.CloseConnection(); }
        }
        public void AgregarFavorito(Articulo articulo, Usuario usuario)
        {
            AccesoBaseDatos datos = new AccesoBaseDatos();

            try
            {
                datos.SetQuery("Insert into FAVORITOS (IdArticulo, IdUser) values (@IdArticulo, @IdUser)");
                datos.SetParameters("@IdArticulo", articulo.Id);
                datos.SetParameters("@IdUser", usuario.Id);

                datos.ExecuteAction();
            }
            catch (Exception ex)
            {

                throw ex;
            }
            finally { datos.CloseConnection(); }
        }

        public List<Articulo> ListarFavoritos(Usuario usuario)
        {
            AccesoBaseDatos datos = new AccesoBaseDatos();
            List<Articulo> listaFavoritos = new List<Articulo>();

            try
            {
                datos.SetQuery("SELECT F.Id, F.IdArticulo, F.IdUser, A.Nombre, A.Descripcion, A.Precio, M.Id AS IdMarca, M.Descripcion AS Marca, C.Id AS IdCategoria, C.Descripcion AS Categoria, A.ImagenUrl " +
                    "\r\nFROM FAVORITOS F \r\nINNER JOIN ARTICULOS A ON F.IdArticulo = A.Id " +
                    "\r\nINNER JOIN MARCAS M ON A.IdMarca = M.Id " +
                    "\r\nINNER JOIN CATEGORIAS C ON A.IdCategoria = C.Id " +
                    "\r\nWHERE F.IdUser = @Id");
                datos.SetParameters("@Id", usuario.Id);

                datos.ExecuteReader();

                if (datos.Lector != null)
                {
                    while (datos.Lector.Read())
                    {
                        Articulo articulo = new Articulo();
                        articulo.Id = (int)datos.Lector["IdArticulo"];
                        articulo.Nombre = (string)datos.Lector["Nombre"];
                        articulo.Descripcion = (string)datos.Lector["Descripcion"];
                        articulo.Precio = Math.Round((decimal)datos.Lector["Precio"], 3);

                        articulo.Marca = new Marca();
                        articulo.Marca.Id = (int)datos.Lector["IdMarca"];
                        articulo.Marca.Descripcion = (string)datos.Lector["Marca"];

                        articulo.Categoria = new Categoria();
                        articulo.Categoria.Id = (int)datos.Lector["IdCategoria"];
                        articulo.Categoria.Descripcion = (string)datos.Lector["Categoria"];

                        if (!(datos.Lector["ImagenUrl"] is DBNull))
                            articulo.ImagenUrl = (string)datos.Lector["ImagenUrl"];

                        listaFavoritos.Add(articulo);

                    }

                }

                return listaFavoritos;
            }
            catch (Exception ex)
            {

                throw ex;
            }
            finally { datos.CloseConnection(); }
        }

        public void EliminarFavorito(Articulo articulo, Usuario usuario)
        {
            AccesoBaseDatos datos = new AccesoBaseDatos();

            try
            {
                datos.SetQuery("Delete from FAVORITOS where IdArticulo = @IdArticulo and IdUser = @IdUser");
                datos.SetParameters("@IdArticulo", articulo.Id);
                datos.SetParameters("@IdUser", usuario.Id);

                datos.ExecuteAction();
            }
            catch (Exception ex)
            {

                throw ex;
            }
            finally { datos.CloseConnection(); }
        }
    }
}
