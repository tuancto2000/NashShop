import "./imageList.css";
import { Link } from "react-router-dom";
import { DataGrid } from "@material-ui/data-grid";
import { useState, useEffect } from "react";
import { useLocation } from "react-router-dom";
import { GetById } from "../../services/productService";
import { DeleteImage } from "../../services/imageService";
import { api_url } from "../../config";
export default function ImageList() {
  const [data, setData] = useState([]);
  const [productName, setProductName] = useState([]);
  const location = useLocation();
  const productId = location.id;
  useEffect(() => {
    GetById(productId)
      .then((response) => {
        setProductName(response.name);
        setData([...response.images]);
      })
      .catch((error) => console.log(error));
  }, []);
  const handleDelete = (id) => {
    DeleteImage(productId, id)
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
      headerName: "ProductImage",
      width: 170,
      renderCell: (params) => {
        return (
          <div className="imageListItem">
            <img
              className="imageListImg"
              src={`${api_url}/${params.row.imagePath}`}
              alt=""
            />
            {productName}
          </div>
        );
      },
    },
    {
      field: "caption",
      headerName: "Caption",
      width: 150,
    },
    {
      field: "dateCreated",
      headerName: "DateCreated",
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
                pathname: "/product/" + productId + "/image/" + param.row.id,
                productId: productId,
                id: param.row.id,
                image: {
                  caption: param.row.caption,
                  imagePath: api_url + param.row.imagePath,
                },
              }}
            >
              <button className="imageListEdit">Edit </button>
            </Link>
            <button
              className="imageListDelete"
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
    <div className="imageList">
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
