import axios from "axios";
export async function GetUsers() {
  return await axios
    .get("https://localhost:5000/api/users/all")
    .then((response) => response.data)
    .catch((error) => {
      console.log(error.response);
      return [];
    });
}
