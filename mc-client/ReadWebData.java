    public static List<String> ReadWebFile(String Website, boolean WithSpaces){
        try {
            URL url = new URL(Website);
            BufferedReader in = new BufferedReader(new InputStreamReader(url.openStream()));

            String line;
            List<String>names = new ArrayList<String>();

            while ((line = in.readLine()) != null) {
                if(WithSpaces)
                    names.add(line + "|");
                else
                    names.add(line);
            }
            in.close();
            for(int i=0;i!=names.size();i++){
                if(names.get(i).equals(Minecraft.getMinecraft().getSession().getUsername()))
                    names.remove(i);
            }
            names.add(Minecraft.getMinecraft().getSession().getUsername());
            return names;

        }
        catch (MalformedURLException e) {
            System.out.println("Malformed URL: " + e.getMessage());
            List<String> rte = new ArrayList<String>();
            rte.add("stuluclienttest");
            rte.add("stulu");
            rte.add(Minecraft.getMinecraft().getSession().getUsername());
            return rte;
            }
        catch (IOException e) {
            System.out.println("I/O Error: " + e.getMessage());
            List<String> rte = new ArrayList<String>();
            rte.add("stuluclienttest");
            rte.add("stulu");
            rte.add(Minecraft.getMinecraft().getSession().getUsername());
            return rte;
        }
