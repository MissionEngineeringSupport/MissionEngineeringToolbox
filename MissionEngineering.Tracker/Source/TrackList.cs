﻿namespace MissionEngineering.Tracker;

public class TrackList
{
    public List<Track> Tracks { get; set; }

    public TrackList()
    {
        Tracks = new List<Track>();
    }

    public void AddTrack(Track track)
    {
        Tracks.Add(track);
    }

    public void DeleteTrack(Track track)
    {
        Tracks.Remove(track);
    }

    public void DeleteTrack(int trackId)
    {
        var track = GetTrackById(trackId);

        Tracks.Remove(track);
    }

    public void ClearTracks()
    {
        Tracks.Clear();
    }

    public Track GetTrackById(int trackId)
    {
        return Tracks.FirstOrDefault(t => t.TrackDataSmoothed.TrackId == trackId);
    }
}