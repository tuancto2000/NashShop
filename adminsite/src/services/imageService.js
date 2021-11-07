import axios from "axios";
import { api_url } from "../config";

export async function GetById(productId, id) {
  return axios({
    method: "get",
    url: "/api/products/" + productId + "/images/" + id,
  })
    .then((response) => {
      return response.data;
    })
    .catch((error) => {
      console.log(error.response);
      return null;
    });
}
export async function PostImage(productId, formData) {
  return axios({
    method: "post",
    url: "/api/products/" + productId + "/images/",
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
export async function PutImage(productId, id, formData) {
  return axios({
    method: "put",
    url: "/api/products/" + productId + "/images/" + id,
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
export const DeleteImage = (productId, id) => {
  return axios({
    method: "delete",
    url: "/api/products/" + productId + "/images/" + id,
  })
    .then((response) => {
      return response.data;
    })
    .catch((error) => {
      console.log(error.response);
      return null;
    });
};
