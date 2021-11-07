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
  return await axios({
    method: "get",
    url: "/api/products/all",
  })
    .then((response) => response.data)
    .catch((error) => {
      console.log(error.response);
      return [];
    });
}
export async function GetById(id) {
  return axios({
    method: "get",
    url: "/api/products/" + id,
  })
    .then((response) => {
      return response.data;
    })
    .catch((error) => {
      console.log(error.response);
      return null;
    });
}
export async function PostProduct(formData) {
  return axios({
    method: "post",
    url: "/api/products/",
    data: formData,
  })
    .then((response) => {
      return response.data;
    })
    .catch((error) => {
      console.log(error.response);
      return null;
    });
}
export async function PutProduct(id, formData) {
  return axios({
    method: "put",
    url: "/api/products/" + id,
    data: formData,
    headers: { "Content-Type": "multipart/form-data" },
  })
    .then((response) => {
      return response.data;
    })
    .catch((error) => {
      console.log(error.response);
      return null;
    });
}
export const DeleteProduct = (id) => {
  return axios({
    method: "delete",
    url: "/api/products/" + id,
  })
    .then((response) => {
      return response.data;
    })
    .catch((error) => {
      console.log(error.response);
      return null;
    });
};
