import { Link } from "react-router-dom";
import "./product.css";
import { Publish } from "@material-ui/icons";
import { useForm } from "react-hook-form";
import { useLocation } from "react-router-dom";

export default function Product() {
  const { register, handleSubmit, errors } = useForm();
  const onHandleSubmit = (data) => {
    console.log(data);
  };
  const location = useLocation();
  const product = location.product;
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
            <input value={product.id} {...register("id", { required: true })} />
            <label> Name</label>
            <input
              value={product.name}
              {...register("name", { required: true })}
            />
            <label> Price</label>
            <input
              value={product.price}
              {...register("price", { required: true })}
            />
            <label> Category</label>
            <input
              value={product.categoryName}
              readOnly
              {...register("category", { required: true })}
            />
            <label> Description</label>
            <input
              value={product.description}
              {...register("description", { required: true })}
            />
            <input type="submit" name="Update" />
          </div>
          <div className="productFormRight">
            <div className="productUpload">
              <img
                src={`${product.image}`}
                alt=""
                className="productUploadImg"
              />
              <label for="file">
                <Publish />
              </label>
              <input
                type="file"
                id="file"
                style={{ display: "none" }}
                {...register("image", { required: true })}
              />
            </div>
          </div>
        </form>
      </div>
    </div>
  );
}
