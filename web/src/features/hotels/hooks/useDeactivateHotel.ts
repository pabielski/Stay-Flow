import { useMutation, useQueryClient } from "@tanstack/react-query";
import { deactivateHotel, hotelKeys } from "../api/hotelsApi";

export function useDeactivateHotel() {
  const queryClient = useQueryClient();

  return useMutation({
    mutationFn: deactivateHotel,
    onSuccess: () => {
      queryClient.invalidateQueries({ queryKey: hotelKeys.all });
    },
  });
}
