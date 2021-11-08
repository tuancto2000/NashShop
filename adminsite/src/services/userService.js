import axios from "axios";
export async function GetUsers() {
  return await axios
    .get("/api/users/all")
    .then((response) => response.data)
    .catch((error) => {
      console.log(error.response);
      return [];
    });
}

export async function Authenticate(request) {
  return axios({
    method: "post",
    url: "/api/users/admin/login",
    data: request,
  })
    .then((response) => {
      return response.data;
    })
    .catch((error) => {
      console.log(error.response);
      return null;
    });
}
