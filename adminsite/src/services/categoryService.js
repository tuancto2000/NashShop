import axios from "axios";
import { api_url } from "../config";

export async function GetCategories() {
  return await axios({
    method: "get",
    url: api_url + "/api/categories/",
  })
    .then((response) => response.data)
    .catch((error) => {
      console.log(error.response);
      return [];
    });
}
export async function PostCategory(formData) {
  return axios({
    method: "post",
    url: api_url + "/api/categories/",
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
export async function PutCategory(id, formData) {
  return axios({
    method: "put",
    url: api_url + "/api/categories/" + id,
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
export const DeleteCategory = (id) => {
  return axios({
    method: "delete",
    url: api_url + "/api/categories/" + id,
  })
    .then((response) => {
      return response.data;
    })
    .catch((error) => {
      console.log(error.response);
      return null;
    });
};
