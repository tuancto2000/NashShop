import "./productList.css";
import { DataGrid } from "@material-ui/data-grid";
import { Link } from "react-router-dom";
import { useState, useEffect } from "react";
import { GetProducts, DeleteProduct } from "../../services/productService";
import { api_url } from "../../config";
export default function ProductList() {
  const [data, setData] = useState([]);

  useEffect(() => {
    GetProducts()
      .then((response) => setData([...response]))
      .catch((error) => console.log(error));
  }, []);
  const handleDelete = (id) => {
    DeleteProduct(id)
      .then((response) => setData([...response]))
      .catch((error) => console.log(error));
    setData(data.filter((item) => item.id !== id));
  };

  useEffect(() => {
    console.log(data);
  }, [data]);

  const columns = [
    { field: "id", headerName: "ID", width: 90 },
    {
      field: "product",
      headerName: "Product",
      width: 170,
      renderCell: (params) => {
        return (
          <div className="productListItem">
            <img
              className="productListImg"
              src={`${api_url}/${params.row.imagePath}`}
              alt=""
            />
            {params.row.name}
          </div>
        );
      },
    },
    { field: "star", headerName: "Star", width: 120 },
    {
      field: "category",
      headerName: "Category",
      width: 130,
      valueGetter: (param) => param.row.productCategory.name,
    },
    {
      field: "price",
      headerName: "Price",
      width: 120,
    },
    {
      field: "dateCreated",
      headerName: "DateCreated",
      width: 200,
    },
    {
      field: "dateUpdated",
      headerName: "DateUpdated",
      width: 200,
    },
    {
      field: "action",
      headerName: "Action",
      width: 250,
      renderCell: (param) => {
        return (
          <>
            <Link
              to={{
                pathname: "/product/" + param.row.id,
                id: param.row.id,
                product: {
                  name: param.row.name,
                  description: param.row.description,
                  price: param.row.price,
                  categoryId: param.row.productCategory.id,
                  categoryName: param.row.productCategory.name,
                  image: api_url + param.row.imagePath,
                },
              }}
            >
              <button className="productListEdit">Edit </button>
            </Link>
            <Link
              to={{
                pathname: "/product/" + param.row.id + "/images/",
                id: param.row.id,
              }}
            >
              <button className="productListEdit">Manage Image </button>
            </Link>
            <button
              className="productListDelete"
              onClick={() => handleDelete(param.row.id)}
            >
              Delete
            </button>
          </>
        );
      },
    },
  ];
  return (
    <div className="productList">
      <DataGrid
        rows={data}
        disableSelectionOnClick
        columns={columns}
        pageSize={8}
        checkboxSelection
      />
    </div>
  );
}
