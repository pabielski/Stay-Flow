import { createBrowserRouter } from "react-router";
import { PanelLayout } from "../layouts/PanelLayout";
import { DashboardPage } from "../pages/DashboardPage";
import { HotelsPage } from "../pages/HotelsPage";
import { NotFoundPage } from "../pages/NotFoundPage";

export const router = createBrowserRouter([
  {
    path: "/",
    element: <PanelLayout />,
    children: [
      { index: true, element: <DashboardPage /> },
      { path: "hotels", element: <HotelsPage /> },
      { path: "*", element: <NotFoundPage /> },
    ],
  },
]);
