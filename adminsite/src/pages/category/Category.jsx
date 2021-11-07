import { Link } from "react-router-dom";
import "./category.css";
import { useForm } from "react-hook-form";
import { useLocation } from "react-router-dom";
import React, { useEffect } from "react";
import { PostCategory, PutCategory } from "../../services/categoryService";
export default function Category() {
  const location = useLocation();
  const category = location.category;
  const id = location.id;
  const { register, handleSubmit, errors } = useForm({
    defaultValues: {
      id: id,
      name: category.name,
      description: category.description,
    },
  });
  const fileInput = React.createRef();
  const onHandleSubmit = (data) => {
    const fd = new FormData();
    for (var key in data) {
      fd.append(key, data[key]); // formdata doesn't take objects
    }
    if (fileInput.current.files[0]) {
      fd.append(
        "image",
        fileInput.current.files[0],
        fileInput.current.files[0].name
      );
    }
    if (!fd.get("id")) {
      PostCategory(fd)
        .then((response) => console.log(response))
        .catch((error) => console.log(error));
    } else {
      PutCategory(fd.get("id"), fd)
        .then((response) => console.log(response))
        .catch((error) => console.log(error));
    }
  };
  return (
    <div className="category">
      <div className="categoryBottom">
        <form
          action=""
          className="categoryForm"
          onSubmit={handleSubmit(onHandleSubmit)}
        >
          <div className="categoryFormLeft">
            <label> Id</label>
            <input {...register("id", { required: false })} />
            <label> Name</label>
            <input {...register("name", { required: true })} />
            <label> Description</label>
            <input {...register("description", { required: true })} />
            <button className="categoryButton" type="submit" name="Update">
              Update or Create
            </button>
          </div>
          <div className="categoryFormRight">
            <div className="categoryUpload">
              <img
                src={`${category.image}`}
                alt=""
                className="categoryUploadImg"
              />
            </div>
          </div>
          <div className="loadImage">
            <label>Select a Photo</label>
            <input type="file" id="avatar" name="avatar" ref={fileInput} />
          </div>
        </form>
      </div>
    </div>
  );
}
