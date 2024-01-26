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
        public List<Articulo> Listar(string id = "")
        {
            List<Articulo> listaArticulos = new List<Articulo>();
            AccesoBaseDatos datos = new AccesoBaseDatos();

            try
            {
                string consulta = "Select A.Id, Codigo, Nombre, A.Descripcion, IdMarca, IdCategoria, ImagenUrl, Precio,\r\nC.Descripcion as Categoria,\r\nM.Descripcion as Marca\r\nfrom ARTICULOS A, CATEGORIAS C, MARCAS M\r\nwhere IdMarca = M.Id and IdCategoria = C.Id";
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
    }
}
