import "./userList.css";
import { DataGrid } from "@material-ui/data-grid";
import { useState, useEffect } from "react";
import { GetUsers } from "../../services/userService";
export default function UserList() {
  const [data, setData] = useState([]);
  useEffect(() => {
    GetUsers()
      .then((response) => setData([...response]))
      .catch((error) => console.log(error));
  }, []);
  useEffect(() => {
    console.log(data);
  }, [data]);
  const handleDelete = (id) => {
    setData(data.filter((item) => item.id !== id));
  };

  const columns = [
    { field: "id", headerName: "ID", width: 250 },
    {
      field: "firstName",
      headerName: "FirstName",
      width: 150,
    },
    {
      field: "lastName",
      headerName: "LastName",
      width: 150,
    },
    {
      field: "userName",
      headerName: "UserName",
      width: 150,
    },
    {
      field: "dob",
      headerName: "Date of birth",
      width: 200,
    },
  ];

  return (
    <div className="userList">
      <DataGrid
        rows={data}
        disableSelectionOnClick
        columns={columns}
        pageSize={5}
        checkboxSelection
      />
    </div>
  );
}
