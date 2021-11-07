import "./image.css";
import { useForm } from "react-hook-form";
import { useLocation } from "react-router-dom";
import { PostImage, PutImage } from "../../services/imageService";
import React from "react";
export default function Image() {
  const location = useLocation();
  const image = location.image;
  const id = location.id;
  const productId = location.productId;
  const { register, handleSubmit } = useForm({
    defaultValues: {
      id: id,
      caption: image.caption,
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
      PostImage(productId, fd)
        .then((response) => console.log(response))
        .catch((error) => console.log(error));
    } else {
      PutImage(productId, fd.get("id"), fd)
        .then((response) => console.log(response))
        .catch((error) => console.log(error));
    }
    console.log(fd);
  };
  return (
    <div className="image">
      <div className="imageBottom">
        <form
          action=""
          className="imageForm"
          onSubmit={handleSubmit(onHandleSubmit)}
        >
          <div className="imageFormLeft">
            <label> Id</label>
            <input {...register("id", { required: false })} />
            <label> caption</label>
            <input {...register("caption", { required: true })} />

            <button className="imageButton" type="submit" name="Update">
              Update or Create
            </button>
          </div>
          <div className="imageFormRight">
            <div className="imageUpload">
              <img
                src={`${image.imagePath}`}
                alt=""
                className="imageUploadImg"
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
