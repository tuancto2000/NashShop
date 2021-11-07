import axios from "axios";

export async function GetCategories() {
  return await axios({
    method: "get",
    url: "/api/categories/",
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
    url: "/api/categories/",
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
    url: "/api/categories/" + id,
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
    url: "/api/categories/" + id,
  })
    .then((response) => {
      return response.data;
    })
    .catch((error) => {
      console.log(error.response);
      return null;
    });
};
