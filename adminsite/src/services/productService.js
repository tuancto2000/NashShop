import axios from "axios";
// export async function getProductPaging() {
//   return await axios({
//     method: "GET",
//     url: "https://localhost:5000/api/products/featured/5",
//     data: null,
//     mode: "no-cors",
//   }).then((res) => res.data);
// }

export async function GetProducts() {
  return await axios
    .get("https://localhost:5000/api/products/featured/5")
    .then((response) => response.data)
    .catch((error) => {
      console.log(error.response);
      return [];
    });
}
