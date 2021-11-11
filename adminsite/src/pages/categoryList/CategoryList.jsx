import "./categoryList.css";
import { DataGrid } from "@material-ui/data-grid";
import { Link } from "react-router-dom";
import { useState, useEffect } from "react";
import { GetCategories, DeleteCategory } from "../../services/categoryService";
import { api_url } from "../../config";
export default function ProductList() {
  const [data, setData] = useState([]);

  useEffect(() => {
    GetCategories()
      .then((response) => setData([...response]))
      .catch((error) => console.log(error));
  }, []);
  const handleDelete = (id) => {
    DeleteCategory(id)
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
      field: "category",
      headerName: "Category",
      width: 170,
      renderCell: (params) => {
        return (
          <div className="categoryListItem">
            <img
              className="categoryListImg"
              src={`${api_url}/${params.row.image}`}
              alt=""
            />
            {params.row.name}
          </div>
        );
      },
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
      width: 150,
      renderCell: (param) => {
        return (
          <>
            <Link
              to={{
                pathname: "/category/" + param.row.id,
                id: param.row.id,
                category: {
                  name: param.row.name,
                  image: api_url + param.row.image,
                  description: param.row.description,
                },
              }}
            >
              <button className="categoryListEdit">Edit </button>
            </Link>
            <button
              className="categoryListDelete"
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
