import "./sidebar.css";
import {
  PermIdentity,
  Storefront,
  LineStyle,
  AccountBalance,
} from "@material-ui/icons";
import { Link } from "react-router-dom";

export default function Sidebar() {
  return (
    <div className="sidebar">
      <div className="sidebarWrapper">
        <div className="sidebarMenu">
          <h3 className="sidebarTitle">Management</h3>

          <ul className="sidebarList">
            <Link to="/" className="link">
              <li className="sidebarListItem">
                <AccountBalance className="sidebarIcon" />
                Home
              </li>
            </Link>
            <Link to="/users" className="link">
              <li className="sidebarListItem">
                <PermIdentity className="sidebarIcon" />
                Users
              </li>
            </Link>
            <Link to="/products" className="link">
              <li className="sidebarListItem">
                <Storefront className="sidebarIcon" />
                Products
              </li>
            </Link>
            <Link to="/categories" className="link">
              <li className="sidebarListItem">
                <LineStyle className="sidebarIcon" />
                Categories
              </li>
            </Link>
          </ul>
        </div>
      </div>
    </div>
  );
}
