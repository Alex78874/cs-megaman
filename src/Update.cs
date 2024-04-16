using System.Numerics;
using Raylib_cs;

class Update {
    // Update the player
    public static void UpdatePlayer(Player player, Cursor cursor) {
        Input.HandleInput(player, cursor);
        cursor.Update();
        player.UpdateMovementState();
    }

    // Update the map
    public static void UpdateMap(Map map) {
        map.Update();
    }

    // Update the instances
    public static void UpdateInstances(List<Item> instances) {
        foreach (Item instance in instances) {instance.Update();}
    }

    // Update the game
    public static void UpdateGame(Player player, Cursor cursor, Map map, List<Item> instances) {
        UpdatePlayer(player, cursor);
        UpdateMap(map);
        UpdateInstances(instances);

        // Get the mouse position on the grid
        // And create a item here
        if (Raylib.IsMouseButtonPressed(MouseButton.Left)) {
            Vector2 mouseGridPosition = Utils.GetMouseGridPosition(32);
            // Create a new item if not colliding with any other item
            if (instances.Exists(item => item.position == mouseGridPosition)) {return;}

            Item item = new Item(30, 1);
            item.position = mouseGridPosition;
            instances.Add(item);
        }
    }
}