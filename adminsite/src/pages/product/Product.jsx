import "./product.css";
import { useForm } from "react-hook-form";
import { useLocation } from "react-router-dom";
import React, { useState, useEffect } from "react";
import { PostProduct, PutProduct } from "../../services/productService";
import { GetCategories } from "../../services/categoryService";

export default function Product() {
  const location = useLocation();
  const product = location.product;
  const id = location.id;

  const [category, setCategory] = useState([]);
  useEffect(() => {
    GetCategories()
      .then((response) => setCategory([...response]))
      .catch((error) => console.log(error));
  }, []);
  useEffect(() => {
    console.log(category);
  }, [category]);
  const { register, handleSubmit } = useForm({
    defaultValues: {
      id: id,
      name: product.name,
      price: product.price,
      category: product.categoryName,
      description: product.description,
      categoryId: product.categoryId,
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
      PostProduct(fd)
        .then((response) => console.log(response))
        .catch((error) => console.log(error));
    } else {
      PutProduct(fd.get("id"), fd)
        .then((response) => console.log(response))
        .catch((error) => console.log(error));
    }
    console.log(fd);
  };
  return (
    <div className="product">
      <div className="productBottom">
        <form
          action=""
          className="productForm"
          onSubmit={handleSubmit(onHandleSubmit)}
        >
          <div className="productFormLeft">
            <label> Id</label>
            <input {...register("id", { required: false })} />
            <label> Name</label>
            <input {...register("name", { required: true })} />
            <label> Price</label>
            <input {...register("price", { required: true })} />
            <label> Category</label>
            <select {...register("categoryId", { required: true })}>
              <option value="">Select...</option>
              {category &&
                category.map((category) => (
                  <option value={category.id}>{category.name}</option>
                ))}
            </select>
            <label> Description</label>
            <input {...register("description", { required: true })} />
            <button className="productButton" type="submit" name="Update">
              Update or Create
            </button>
          </div>
          <div className="productFormRight">
            <div className="productUpload">
              <img
                src={`${product.image}`}
                alt=""
                className="productUploadImg"
              />
            </div>
            <label>Select a Photo</label>
            <input type="file" id="avatar" ref={fileInput} />
          </div>
        </form>
      </div>
    </div>
  );
}
