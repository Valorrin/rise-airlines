using Airlines.Business.Models;


namespace Airlines.Business;

public class FlightTreeNode
{
    public string Airport { get; }
    public List<FlightTreeNode> Children { get; }

    public FlightTreeNode(string airport)
    {
        Airport = airport;
        Children = [];
    }

    public void AddChild(FlightTreeNode child) => Children.Add(child);
}

public class FlightRouteTree
{
    public FlightTreeNode Root { get; }

    public FlightRouteTree(string startAirportId) => Root = new FlightTreeNode(startAirportId);

    public void AddFlight(Flight flight) => AddFlightRecursive(Root, flight);

    private bool AddFlightRecursive(FlightTreeNode node, Flight flight)
    {
        if (node == null)
            return false;

        if (node.Airport == flight.DepartureAirport)
        {
            node.AddChild(new FlightTreeNode(flight.ArrivalAirport));
            return true;
        }

        foreach (var child in node.Children)
        {
            if (AddFlightRecursive(child, flight))
                return true;
        }

        return false;
    }

    public bool RemoveLastFlight() => RemoveLastFlightRecursive(Root, null);

    private bool RemoveLastFlightRecursive(FlightTreeNode node, FlightTreeNode? previous)
    {
        if (node == null)
            return false;

        if (node.Children.Count == 0)
        {
            previous?.Children.RemoveAt(previous.Children.Count - 1);
            return true;
        }

        var lastChild = node.Children[^1];
        return RemoveLastFlightRecursive(lastChild, node);
    }

    public void PrintRoute()
    {
        var route = new List<string>();
        _ = PrintRouteRecursive(Root, route);
        Console.WriteLine("Route: " + string.Join(" -> ", route));
    }

    private bool PrintRouteRecursive(FlightTreeNode node, List<string> route)
    {
        if (node == null)
            return false;

        route.Add(node.Airport);

        if (node.Children.Count == 0)
        {
            return true;
        }

        foreach (var child in node.Children)
        {
            if (PrintRouteRecursive(child, route))
                return true;
        }

        return false;
    }

    public bool FindRouteAndDisplay(string destinationAirport)
    {
        var route = new List<Flight>();

        var routeExists = FindRouteRecursive(Root, destinationAirport, route);

        if (routeExists)
        {
            route.Reverse();
            Console.WriteLine($"Route to {destinationAirport} found:");
            foreach (var flight in route)
            {
                Console.WriteLine($"Flight from {flight.DepartureAirport} to {flight.ArrivalAirport}");
            }
        }
        else
        {
            Console.WriteLine($"No route to {destinationAirport} found.");
        }

        return routeExists;
    }

    private bool FindRouteRecursive(FlightTreeNode node, string destinationAirport, List<Flight> route)
    {
        if (node == null)
            return false;

        if (node.Airport == destinationAirport)
            return true;

        foreach (var child in node.Children)
        {
            var routeExists = FindRouteRecursive(child, destinationAirport, route);

            if (routeExists)
            {
                route.Add(new Flight() { Id = "FlightID", DepartureAirport = node.Airport, ArrivalAirport = child.Airport });
                return true;
            }
        }

        return false;
    }
}
