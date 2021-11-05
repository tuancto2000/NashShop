import "./productList.css";
import { DataGrid } from "@material-ui/data-grid";
import { DeleteOutline } from "@material-ui/icons";
import { Link } from "react-router-dom";
import { useState, useEffect } from "react";
import { GetProducts } from "../../services/productService";
import { api_url } from "../../config";
export default function ProductList() {
  const [data, setData] = useState([]);

  useEffect(() => {
    GetProducts()
      .then((response) => setData([...response]))
      .catch((error) => console.log(error));
  }, []);
  const handleDelete = (id) => {
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
    { field: "star", headerName: "Star", width: 150 },
    {
      field: "category",
      headerName: "Category",
      width: 150,
      valueGetter: (param) => param.row.productCategory.name,
    },
    {
      field: "price",
      headerName: "Price",
      width: 160,
    },
    {
      field: "action",
      headerName: "Action",
      width: 150,
      renderCell: (params) => {
        return (
          <>
            <Link to={"/product/" + params.row.id}>
              <button className="productListEdit">Edit</button>
            </Link>
            <Link to={"/product/" + params.row.id}>
              <button
                className="productListDelete"
                onClick={() => handleDelete(params.row.id)}
              >
                Delete
              </button>
            </Link>
          </>
        );
      },
    },
  ];
  return (
    <div className="productList">
      <Link to="/newproduct">
        <button className="productAddButton">Create</button>
      </Link>
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
