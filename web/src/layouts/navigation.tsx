import type { ReactNode } from "react";
import DashboardIcon from "@mui/icons-material/Dashboard";
import HotelIcon from "@mui/icons-material/Hotel";

export type NavigationItem = {
  label: string;
  path: string;
  icon: ReactNode;
};

export const navigationItems: NavigationItem[] = [
  { label: "Pulpit", path: "/", icon: <DashboardIcon /> },
  { label: "Hotele", path: "/hotels", icon: <HotelIcon /> },
];
